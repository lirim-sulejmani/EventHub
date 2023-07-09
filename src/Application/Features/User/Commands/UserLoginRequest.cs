using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.Tokens.Dtos;
using Carmax.Domain.Helpers;
using MediatR;
using Carmax.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Carmax.Application.Features.User.Commands;
public class UserLoginRequest : IRequest<UserTokenResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public class UserLoginRequestHandler : IRequestHandler<UserLoginRequest, UserTokenResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        public UserLoginRequestHandler(IApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        public async Task<UserTokenResponse> Handle(UserLoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                {
                    return new UserTokenResponse { Message = "Email or password cannot be empty!" };
                }
                var user = _context.Users.FirstOrDefault(x => x.Email == request.Email);
                if (user is null)
                {
                    return await Task.FromResult(new UserTokenResponse() { Success = false, Message = "Authentication Failed" });
                }
                if (user.StatusId == StatusEnum.Passive)
                {
                    return await Task.FromResult(new UserTokenResponse() { Success = false, Message = "User is Passive" });
                }
                if (!HashHelper.VerifyPasswordHash(request.Password, user.Password, user.Salt))
                {
                    return await Task.FromResult(new UserTokenResponse() { Success = false, Message = "Email address or password is wrong!" });
                }
                return await _tokenService.GenerateTokensAndUpdateUser(user, false);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new UserTokenResponse() { Success = false, Message = ex.Message });
            }
        }
    }
}