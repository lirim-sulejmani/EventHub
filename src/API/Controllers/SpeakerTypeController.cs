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
using Carmax.Application.Features.SpeakerType.Commands;
using Carmax.Application.Features.SpeakerType.Dtos;
using Carmax.Application.Features.SpeakerType.Queries;

namespace API.Controllers;

public class SpeakerTypeController : ApiControllerBase
{
    public readonly IConfiguration _config;

    public SpeakerTypeController(IConfiguration config)
    {
        _config = config;
    }
    [HttpPost]
    [Route("newSpeaker")]
    [AllowAnonymous]
    public async Task<ActionResult> AddSpeakerType(AddSpeakerTypeRequest request)
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
    [Route("updateSpeakerType")]
    public async Task<ResponseDto> UpdateSpeaker(UpdateSpeakerTypeRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UpdateSpeakerTypeRequest");
            throw;
        }
    }


    [HttpGet]
    [Route("list")]
    public async Task<SpeakerTypeDto> GetSpeakerTypeLists(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetSpeakerTypeListRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }



    [HttpGet]
    [Route("getSpeakerTypeDetails")]
    public async Task<SpeakerTypeDto> GetSpeakerDetails(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetSpeakerTypeDetailsRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    [HttpPost]
    [Route("deleteSpeakerType")]
    public async Task<ResponseDto> DeleteSpeakerType(Guid id)
    {

        try
        {
            return await Mediator.Send(new DeleteSpeakerTypeRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }
}
