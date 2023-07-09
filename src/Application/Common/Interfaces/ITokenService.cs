using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Tokens.Commands;
using Carmax.Application.Features.Tokens.Dtos;
using Carmax.Application.Features.User.Dtos;
using Carmax.Domain.Entities;

namespace Carmax.Application.Common.Interfaces;
public interface ITokenService
{
    Task<UserTokenResponse> GetTokenAsync(string email, string password);
    Task<UserTokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
    Task<UserTokenResponse> UserRefreshTokenAsync(RefreshTokenRequest request);
    Task<UserTokenResponse> GenerateTokensAndUpdateUser(User? user, bool isRefresh);
    Task<UserDto> GetCurrentlyLoggedInUser();
    bool IsAuthenticated();
}
