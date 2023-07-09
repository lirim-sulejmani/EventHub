using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Carmax.Application.Common.Exceptions;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.Tokens.Commands;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Carmax.Application.Common.Exceptions;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.User.Dtos;
using Carmax.Application.Features.Tokens.Commands;
using Carmax.Application.Features.Tokens.Dtos;
using Carmax.Domain.Entities;
using Carmax.Domain.Enums;
using Carmax.Domain.Helpers;
using Carmax.Infrastructure.Auth;
using Carmax.Infrastructure.Auth.Jwt;
using Carmax.Infrastructure.Identity;
using Carmax.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Data.Entity;
using static Duende.IdentityServer.Models.IdentityResources;

namespace Carmax.Infrastructure.Services;
public class TokenService : ITokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    public TokenService(
        IUserService userService,
        IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, IConfiguration configuration)
    {
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
        _context = context;
        _configuration = configuration;
    }

    public async Task<UserTokenResponse> GetTokenAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user is null)
        {
            throw new Exception("Authentication Failed");
        }
        if (user.StatusId == StatusEnum.Passive)
        {
            throw new Exception("User is not active");
        }
        if (!HashHelper.VerifyPasswordHash(password,user.Password,user.Salt))
        {
            throw new Exception("Email address or password is wrong!");
        }
        return await GenerateTokensAndUpdateUser(user, false);
    }

    public async Task<UserTokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
    {
        try
        {
            var userPrincipal = GetPrincipalFromExpiredToken(request.Token);
            string? userEmail = userPrincipal.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
            if (user is null)
            {
                throw new UnauthorizedException("auth.failed");
            }
            if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new UnauthorizedException("identity.invalidrefreshtoken");
            }
            return await GenerateTokensAndUpdateUser(user, true);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "RefreshTokenAsync");
            throw;
        }
    }
    public async Task<UserTokenResponse> UserRefreshTokenAsync(RefreshTokenRequest request)
    {
        var userPrincipal = GetPrincipalFromExpiredToken(request.Token);
        string? userEmail = userPrincipal.FindFirst(ClaimTypes.Email)?.Value;
        var user = _context.Users.FirstOrDefault(x => x.Email == userEmail);
        if (user is null)
        {
            throw new UnauthorizedException("auth.failed");
        }

        if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new UnauthorizedException("identity.invalidrefreshtoken");
        }
        return await GenerateTokensAndUpdateUser(user, true);
    }


    public async Task<UserTokenResponse> GenerateTokensAndUpdateUser(User user, bool isRefresh)
    {
        string token = GenerateUserJwt(user);
        DateTime? refreshTokenExpiryTime = !isRefresh ? DateTime.Now.AddDays(1) : null;
        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = isRefresh ? user.RefreshTokenExpiryTime : refreshTokenExpiryTime;

        var res = _context.Users.Update(user);
        var response = new UserTokenResponse(user.Id, token, user.RefreshToken, user.RefreshTokenExpiryTime.HasValue ? user.RefreshTokenExpiryTime.Value : null, user.RoleId);
        response.Success = true;
        return response;
    }

    private string GenerateJwt(ApplicationUser user) => GenerateEncryptedToken(GetSigningCredentials(), GetClaims(user));
    private string GenerateUserJwt(Domain.Entities.User user) => GenerateEncryptedToken(GetSigningCredentials(), GetUserClaims(user));

    private IEnumerable<Claim> GetClaims(ApplicationUser user) =>
        new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.FirstName ?? string.Empty),
            new(ClaimTypes.Surname, user.LastName ?? string.Empty),
        };

    private IEnumerable<Claim> GetUserClaims(Domain.Entities.User user) =>
        new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.FirstName ?? string.Empty),
            new(ClaimTypes.Surname, user.LastName ?? string.Empty),
        };

    private string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
           claims: claims,
           expires: DateTime.Now.AddMinutes(1),
           signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        try
        {
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("No Key defined in JwtSettings config.");
            }
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RoleClaimType = ClaimTypes.Role,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new UnauthorizedException("identity.invalidtoken");
            }

            return principal;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private SigningCredentials GetSigningCredentials()
    {
        if (string.IsNullOrEmpty(_configuration["Jwt:Key"]))
        {
            throw new InvalidOperationException("No Key defined in JwtSettings config.");
        }

        byte[] secret = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
    }
    public async Task<UserDto> GetCurrentlyLoggedInUser()
    {
        var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
        var userId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == userId);
        if (user != null)
        {
            return new UserDto()
            {
                UserId = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Success = true,
            };
        }

        return new UserDto() { Success = false, Message = "No user logged in!" };
    }

    public bool IsAuthenticated()
    {
        var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
        return identity != null && !string.IsNullOrWhiteSpace(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}