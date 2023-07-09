using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Domain.Enums;

namespace Carmax.Application.Features.Tokens.Dtos;
public class TokenResponse : ResponseDto
{
    public Guid Id { get; }
    public string Token { get; }
    public string RefreshToken { get; }
    public DateTimeOffset? RefreshTokenExpiryTime { get; }
    public UserRole? RoleId { get; }
    public UserStatus? StatusId { get; }

    public TokenResponse()
    {

    }
    public TokenResponse(Guid userId, string token, string refreshToken, DateTimeOffset? refreshTokenExpiryTime, UserRole? roleId)
    {
        Id = userId;
        Token = token;
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = refreshTokenExpiryTime;
        RoleId = roleId;
    }
}

public class UserTokenResponse : ResponseDto
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
    public UserRole? RoleId { get; }
    public UserStatus? StatusId { get; }
    public UserTokenResponse()
    {

    }
    public UserTokenResponse(Guid id, string token, string refreshToken, DateTimeOffset? refreshTokenExpiryTime, UserRole? roleId)
    {
        Id = id;
        Token = token;
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = refreshTokenExpiryTime;
        RoleId = roleId;
    }
}

