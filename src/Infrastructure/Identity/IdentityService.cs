using System.Threading;
using Azure.Core;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.User.Dtos;
using Carmax.Domain.Entities;
using Carmax.Domain.Enums;
using Duende.IdentityServer.Endpoints.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Carmax.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;


    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user?.UserName;
    }


    public async Task<ResponseDto> UpdateUserAsync(UserDto userData)
    {
        var user = await _userManager.FindByIdAsync(userData.UserId);
        if (user != null)
        {
            user.Email = userData.Email;
            user.FirstName = userData.FirstName;
            user.LastName = userData.LastName;
            user.City = userData.City;
            user.Address = userData.Address;
            user.PhoneNumber = userData.PhoneNumber;
            user.RoleId = userData.RoleId;
            user.StatusId = userData.StatusId;
            var res = await _userManager.UpdateAsync(user);
            if (res.Succeeded)
            {
                return new ResponseDto() { Success = true, Message = "User updated succesfully." };
            }
            return new ResponseDto() { Success = false, Message = "User failed to update!" };
        }
        return new ResponseDto() { Success = false, Message = "Error! No user found with the provided ID!" };
    }

    public async Task<ResponseDto> ChangePasswordAsync(string userId, string currentPassword, string password, string confirmPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var isValid = password == confirmPassword;
            if (isValid)
            {
                var res = await _userManager.ChangePasswordAsync(user, currentPassword, password);
                if (res.Succeeded)
                {
                    return new ResponseDto() { Success = true, Message = "Password changed succesfully!" };
                }
                else
                {
                    return new ResponseDto() { Success = false, Message = res.Errors.FirstOrDefault().Description };
                }
            }
            return new ResponseDto() { Success = false, Message = "The Confirm Password does not match with New Password!" };
        }
        return new ResponseDto() { Success = false, Message = "Error! No user found with the provided ID!" };
    }



    public async Task<(Result Result, string UserId)> CreateUserAsync(UserDto user, string password)
    {
        var applicationUser = new ApplicationUser
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            City = user.City,
            Address = user.Address,
            RoleId = user.RoleId,
            StatusId = StatusEnum.Active,
        };
        var result = await _userManager.CreateAsync(applicationUser, password);
        return (result.ToApplicationResult(), user.FirstName);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<SignInResult> PasswordSignIn(string userName, string password,
        bool isPersistent, bool lockoutOnFailure)
    {
        var user = await _userManager.FindByEmailAsync(userName);
        if (user == null)
        {
            return SignInResult.Failed;
        }
        return await _signInManager.PasswordSignInAsync(user.UserName, password, isPersistent, lockoutOnFailure);
    }

    public async Task<UsersResponseDto> GetUsersAsync(int pageNumber, string? name, StatusEnum? status, CancellationToken cancellationToken)
    {
        try
        {
            var pageSize = 10;
            int excludeRecords = (pageSize * pageNumber) - pageSize;
            var users = await _userManager.Users.Where(x =>
                (!string.IsNullOrWhiteSpace(name) ? x.UserName.ToLower().Contains(name.ToLower())
               || x.FirstName.ToLower().Contains(name.ToLower()) || x.LastName.ToLower().Contains(name.ToLower()) : true) &&
                ((status != null || status.Equals(StatusEnum.Active)) ? x.StatusId.Equals(status) : true))
                .AsNoTracking()
                .Skip(excludeRecords)
                .Take(10).OrderBy(x => x.UserName)
                .ToListAsync(cancellationToken);
            var userList = users.Select(x => new UserDto()
            {
                Id = Guid.NewGuid(),
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                RoleId = (UserRole)x.RoleId,
                StatusId = x.StatusId,
            }).ToList();
            var response = new UsersResponseDto()
            {
                Users = userList,
                TotalItems = await _userManager.Users.AsNoTracking().CountAsync(cancellationToken),
                PageNumber = pageNumber,
                PageSize = pageSize,
                Success = true,
            };
            return await Task.FromResult(response);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<UserDto> GetUserByIdAsync(string id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return new UserDto()
                {
                    Id = Guid.NewGuid(),
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    City = user.City,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    RoleId = (Domain.Enums.UserRole)user.RoleId,
                    StatusId = (Domain.Enums.StatusEnum)user.StatusId,
                    Success = true,
                };
            }
            return new UserDto() { Success = false, Message = "Error! No user found with the provided ID!" };
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public async Task<bool> CheckIfEmailExists(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            return true;
        }
        return false;
    }

    public async Task<UserDto> UpdateForgotPasswordToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return new UserDto { Message = "No user was found with this email" };
        string code = await _userManager.GeneratePasswordResetTokenAsync(user);
        user.ForgotPasswordToken = code;
        user.ForgotPaswordTokenExpire = DateTime.Now.AddMinutes(10);
        var response = await _userManager.UpdateAsync(user);
        return new UserDto()
        {
            FirstName = user.FirstName,
            Email = user.Email,
            ForgotPasswordToken = user.ForgotPasswordToken,
        };
    }
    public async Task<ResponseDto> ResetUserPassword(string token, string password)
    {
        var user = _userManager.Users.FirstOrDefault(x => x.ForgotPasswordToken == token);
        if (string.IsNullOrWhiteSpace(token) || user == null || user.ForgotPaswordTokenExpire < DateTime.Now)
        {
            return await Task.FromResult(new ResponseDto() { Success = false, Message = "Sesion expired!" });
        }
        var response = await _userManager.ResetPasswordAsync(user, token, password);
        if (response.Succeeded)
        {
            return await Task.FromResult(new ResponseDto() { Success = true, Message = "Your password has been successfully reset!" });
        }
        return await Task.FromResult(new ResponseDto() { Success = false, Message = "Error while trying to reset password!" });
    }
}
