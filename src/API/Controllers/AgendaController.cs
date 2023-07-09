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
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.Event.Queries;

namespace API.Controllers;

public class AgendaController : ApiControllerBase
{
    public readonly IConfiguration _config;

    public AgendaController(IConfiguration config)
    {
        _config = config;
    }
    [HttpPost]
    [Route("newAgenda")]
    [AllowAnonymous]
    public async Task<ActionResult> AddAgenda(AddAgendaRequest request)
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
    [Route("updateAgenda")]
    public async Task<ResponseDto> UpdateCountry(UpdateAgendaRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UpdateAgendaRequest");
            throw;
        }
    }


    [HttpGet]
    [Route("list")]
    public async Task<AgendaDto> GetAgendaLists(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetAgendaListRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }



    [HttpGet]
    [Route("getAgendaDetails")]
    public async Task<AgendaDto> GetAgendaDetails(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetAgendaDetailsRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    [HttpPost]
    [Route("deleteAgenda")]
    public async Task<ResponseDto> DeleteAgenda(Guid id)
    {

        try
        {
            return await Mediator.Send(new DeleteAgendaRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }
}
