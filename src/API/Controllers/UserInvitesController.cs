using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using Carmax.API.Controllers;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.UserInvites.Commands;
using Carmax.Application.Features.UserInvites.Dtos;
using Carmax.Application.Features.UserInvites.Queries;
using Carmax.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using static System.Net.Mime.MediaTypeNames;
using Carmax.Application.Features.User.Commands;
using MediatR;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.Event.Queries;

namespace API.Controllers;


public class UserInvitesController : ApiControllerBase
{
    public readonly IConfiguration _config;

    public UserInvitesController(IConfiguration config)
    {
        _config = config;
    }
    [HttpPost]
    [Route("newUserInvites")]
    [AllowAnonymous]
    public async Task<ActionResult> AddUserInvites(AddUserInvitesRequest request)
    {
        try
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }
    [HttpPost]
    [AllowAnonymous]
    [Route("updateUserInvites")]
    public async Task<ResponseDto> UpdateInvitation(UpdateUserInvitesRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UpdateUserInvitesRequest");
            throw;
        }
    }

    [HttpGet]
    [Route("list")]
    public async Task<UserInvitesDto> GetUserInvitesLists(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetUserInvitesListRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }


    [HttpGet]
    [Route("getUserInvitesDetails")]
    public async Task<UserInvitesDto> GetUserInvitesDetails(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetUserInvitesDetailsRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    [HttpPost]
    [Route("deleteUserInvites")]
    public async Task<ResponseDto> DeleteUserInvites(Guid id)
    {

        try
        {
            return await Mediator.Send(new DeleteUserInvitesRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }
}

