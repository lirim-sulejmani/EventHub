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
using Carmax.Application.Features.AgendaType.Commands;
using Carmax.Application.Features.AgendaType.Queries;
using Carmax.Application.Features.AgendaType.Dtos;

namespace API.Controllers;

public class AgendaTypeController : ApiControllerBase
{
    public readonly IConfiguration _config;

    public AgendaTypeController(IConfiguration config)
    {
        _config = config;
    }
    [HttpPost]
    [Route("newAgendaType")]
    [AllowAnonymous]
    public async Task<ActionResult> AddAgendaType(AddAgendaTypeRequest request)
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
    [Route("updateAgendaType")]
    public async Task<ResponseDto> UpdateCountry(UpdateAgendaTypeRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UpdateAgendaTypeRequest");
            throw;
        }
    }


    [HttpGet]
    [Route("list")]
    public async Task<AgendaTypeDto> GetAgendaTypeLists(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetAgendaTypeListRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }



    [HttpGet]
    [Route("getAgendaTypeDetails")]
    public async Task<AgendaTypeDto> GetAgendaDetails(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetAgendaTypeDetailsRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    [HttpPost]
    [Route("deleteAgendaType")]
    public async Task<ResponseDto> DeleteAgendaType(Guid id)
    {

        try
        {
            return await Mediator.Send(new DeleteAgendaTypeRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }
}
