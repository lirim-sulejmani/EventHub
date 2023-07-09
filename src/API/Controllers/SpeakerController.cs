
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
using Carmax.Application.Features.SocialMedia.Commands;
using Carmax.Application.Features.SocialMedia.Dtos;
using Carmax.Application.Features.SocialMedia.Queries;
using Carmax.Application.Features.Speaker.Commands;
using Carmax.Application.Features.Speaker.Dtos;
using Carmax.Application.Features.Speaker.Queries;

namespace API.Controllers;

public class SpeakerController : ApiControllerBase
{
    public readonly IConfiguration _config;

    public SpeakerController(IConfiguration config)
    {
        _config = config;
    }
    [HttpPost]
    [Route("newSpeaker")]
    [AllowAnonymous]
    public async Task<ActionResult> AddSpeaker(AddSpeakerRequest request)
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
    [Route("updateSpeaker")]
    public async Task<ResponseDto> UpdateSpeaker(UpdateSpeakerRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UpdateSpeakerRequest");
            throw;
        }
    }


    [HttpGet]
    [Route("list")]
    public async Task<SpeakerDto> GetSpeakerLists(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetSpeakerListRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }



    [HttpGet]
    [Route("getSpeakerDetails")]
    public async Task<SpeakerDto> GetSpeakerDetails(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetSpeakerDetailsRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    [HttpPost]
    [Route("deleteSpeaker")]
    public async Task<ResponseDto> DeleteSpeaker(Guid id)
    {

        try
        {
            return await Mediator.Send(new DeleteSpeakerRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }
}
