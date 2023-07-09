using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.User.Dtos;
using Carmax.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Carmax.Application.Common.Interfaces;
public interface IUserService
{
    //Task<bool> AuthorizeAsync(Guid userId, string policy);

    //Task<bool> AuthorizeAsync(Guid userId, string policy);

    //Task<string?> GetUserNameAsync(Guid userId);
    Task<string> GetUserNameAsync(Guid userId);
    //Task<string> GetUserNameAsync(string userId);
    Task<bool> IsInRoleAsync(Guid userId, UserRole role);
    
    //Task<bool> IsInRoleAsync(Guid userId, string v);

    //Task<ResponseDto> UpdateUserAsync(UserDto user);
    //Task<ResponseDto> ChangePasswordAsync(string id, string currentPassword, string password, string confirmPassword);


    //Task<(Result Result, string UserId)> CreateUserAsync(UserDto user, string password);

    //Task<UsersResponseDto> GetUsersAsync(int pageNumber, string? name, StatusEnum? status, CancellationToken cancellationToken);
    //Task<UserDto> GetUserByIdAsync(string id);

    //Task<UserDto> UpdateForgotPasswordToken(string email);
    //Task<ResponseDto> ResetUserPassword(string token, string password);
    //Task<bool> CheckIfEmailExists(string email);
    //Task<Result> DeleteUserAsync(Guid userId);
    //Task<SignInResult> PasswordSignIn(string userName, string password,
    //   bool isPersistent, bool lockoutOnFailure);
}

