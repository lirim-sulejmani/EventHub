using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using Carmax.API.Controllers;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Invitations.Commands;
using Carmax.Application.Features.Invitations.Dtos;
using Carmax.Application.Features.Invitations.Queries;
using Carmax.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using static System.Net.Mime.MediaTypeNames;
using Carmax.Application.Features.User.Commands;
using MediatR;
using Carmax.Application.Features.Template.Dtos;
using Carmax.Application.Features.Template.Queries;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.Event.Queries;

namespace API.Controllers;

public class InvitationController : ApiControllerBase
{
    public readonly IConfiguration _config;

    public InvitationController(IConfiguration config)
    {
        _config = config;
    }
    [HttpPost]
    [Route("newInvitation")]
    [AllowAnonymous]
    public async Task<ActionResult> AddInvitation(AddInvitationRequest request)
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
    [Route("updateInvitation")]
    public async Task<ResponseDto> UpdateInvitation(UpdateInvitationRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UpdateInvitationRequest");
            throw;
        }
    }

    [HttpGet]
    [Route("list")]
    public async Task<InvitationDto> GetInvitationLists(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetInvitationListRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }



    [HttpGet]
    [Route("getInvitationDetails")]
    public async Task<InvitationDto> GetInvitationDetails(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetInvitationDetailsRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    [HttpPost]
    [Route("deleteInvitation")]
    public async Task<ResponseDto> DeleteInvitation(Guid id)
    {

        try
        {
            return await Mediator.Send(new DeleteInvitationRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }
}
