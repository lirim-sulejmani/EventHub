using Carmax.Application.Common.Models;
using Carmax.Application.Features.User.Dtos;
using Carmax.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Carmax.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);
    Task<ResponseDto> UpdateUserAsync(UserDto user);
    Task<ResponseDto> ChangePasswordAsync(string id, string currentPassword, string password, string confirmPassword);
    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(UserDto user, string password);

    Task<UsersResponseDto> GetUsersAsync(int pageNumber, string? name, StatusEnum? status, CancellationToken cancellationToken);
    Task<UserDto> GetUserByIdAsync(string id);

    Task<UserDto> UpdateForgotPasswordToken(string email);
    Task<ResponseDto> ResetUserPassword(string token, string password);
    Task<bool> CheckIfEmailExists(string email);
    Task<Result> DeleteUserAsync(string userId);
    Task<SignInResult> PasswordSignIn(string userName, string password,
       bool isPersistent, bool lockoutOnFailure);
}
