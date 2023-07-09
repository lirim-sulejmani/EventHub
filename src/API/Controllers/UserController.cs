using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using Carmax.API.Controllers;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.User.Commands;
using Carmax.Application.Features.User.Dtos;
using Carmax.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using static System.Net.Mime.MediaTypeNames;
using Carmax.Application.Features.User.Queries;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ApiControllerBase
{
    public readonly IConfiguration _config;
    public UserController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost]
    [Route("newUser")]
    [AllowAnonymous]
    public async Task<ActionResult> AddUser(AddUserRequest request)
    {
        try
        {
            var user = await Mediator.Send(request);
            return Ok(user);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "AddUserRequest");
            throw;
        }

    }

   

    [HttpPost]
    [AllowAnonymous]
    [Route("updateUser")]
    public async Task<ResponseDto> UpdateUser(UpdateUserRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UpdateUserRequest");
            throw;
        }
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("deleteUser")]
    public async Task<ResponseDto> DeleteUser(DeleteUserRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "DeleteUserRequest");
            throw;
        }
    }



    [HttpGet]
    [Route("getUserDetails")]
    public async Task<UserDto> GetUsersDetails(Guid id)
    {
        try
        {
            return await Mediator.Send(new GetUsersDetailsRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, "GetUsersDetailsRequest");
            throw;
        }
    }



    

    [HttpPost]
    [Route("change-password")]
    public async Task<ResponseDto> ChangePassword(ChangeUserPasswordRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "ChangeUserPasswordRequest");
            throw;
        }
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("checkIfUserEmailExists")]
    public async Task<ResponseDto> CheckIfEmailExists(string email)
    {
        try
        {
            return await Mediator.Send(new CheckIfEmailExistsRequest(email));
        }
        catch (Exception ex)
        {
            Log.Error(ex, "CheckIfEmailExistsRequest");
            throw;
        }
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("forgot-password")]
    public async Task<ResponseDto> ForgotPassword(string email)
    {
        try
        {
            return await Mediator.Send(new ForgotPasswordRequest(email));
        }
        catch (Exception ex)
        {
            Log.Error(ex, "ForgotPasswordRequest");
            throw;
        }
    }


    [HttpPost]
    [AllowAnonymous]
    [Route("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        try
        {
            var response = await Mediator.Send(request);
            if (response.Success)
            {
                return Redirect($"{_config["RedirectUrls:UiURL"]}/login");
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }

    }
}
