using System.Security.Claims;
using System.Text;
using Azure;
using Carmax.API.Controllers;
using Carmax.Application.Common.Models;
using Carmax.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using static System.Net.Mime.MediaTypeNames;
using Carmax.Application.Features.User.Commands;
using MediatR;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.Agenda.Commands;
using Carmax.Application.Features.Agenda.Dtos;
using Carmax.Application.Features.Agenda.Queries;
using Carmax.Application.Features.AgendasSpeaker.Commands;
using Carmax.Application.Features.AgendasSpeaker.Dtos;
using Carmax.Application.Features.AgendasSpeaker.Queries;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.Event.Queries;

namespace API.Controllers;

public class AgendasSpeakerController : ApiControllerBase
{
    public readonly IConfiguration _config;

    public AgendasSpeakerController(IConfiguration config)
    {
        _config = config;
    }
    [HttpPost]
    [Route("newAgendasSpeaker")]
    [AllowAnonymous]
    public async Task<ActionResult> AddAgenda(AddAgendasSpeakerRequest request)
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
    [Route("updateAgendaaSpeaker")]
    public async Task<ResponseDto> UpdateCountry(UpdateAgendasSpeakerRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UpdateAgendasSpeakerRequest");
            throw;
        }
    }

    [HttpGet]
    [Route("list")]
    public async Task<AgendasSpeakerDto> GetAgendasSpeakerLists(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetAgendasSpeakerListRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }



    [HttpGet]
    [Route("getAgendasSpeakerDetails")]
    public async Task<AgendasSpeakerDto> GetAgendaDetails(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetAgendasSpeakerDetailsRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    [HttpPost]
    [Route("deleteAgendasSpeaker")]
    public async Task<ResponseDto> DeleteAgendasSpeaker(Guid id)
    {

        try
        {
            return await Mediator.Send(new DeleteAgendasSpeakerRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }
}

