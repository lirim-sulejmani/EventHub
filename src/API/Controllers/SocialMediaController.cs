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

namespace API.Controllers;

public class SocialMediaController : ApiControllerBase
{
    public readonly IConfiguration _config;

    public SocialMediaController(IConfiguration config)
    {
        _config = config;
    }
    [HttpPost]
    [Route("newSocialMedia")]
    [AllowAnonymous]
    public async Task<ActionResult> AddSocialMediaType(AddSocialMediaRequest request)
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
    [Route("updateSocialMedia")]
    public async Task<ResponseDto> UpdateCountry(UpdateSocialMediaRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UpdateSocialMediaRequest");
            throw;
        }
    }


    [HttpGet]
    [Route("list")]
    public async Task<SocialMediaDto> GetASocialMediaLists(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetSocialMediaListRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }



    [HttpGet]
    [Route("getSocialMediaDetails")]
    public async Task<SocialMediaDto> GetSocialMediaDetails(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetSocialMediaDetailsRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    [HttpPost]
    [Route("deleteSocialMedia")]
    public async Task<ResponseDto> DeleteSOcialMedia(Guid id)
    {

        try
        {
            return await Mediator.Send(new DeleteSocialMediaRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }
}
