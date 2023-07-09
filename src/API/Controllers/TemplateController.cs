using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using Carmax.API.Controllers;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Template.Commands;
using Carmax.Application.Features.Template.Dtos;
using Carmax.Application.Features.Template.Queries;
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


public class TemplateController : ApiControllerBase
{
    public readonly IConfiguration _config;

    public TemplateController(IConfiguration config)
    {
        _config = config;
    }
    [HttpPost]
    [Route("newTemplate")]
    [AllowAnonymous]
    public async Task<ActionResult> AddTemplate(AddTemplateRequest request)
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
    [Route("updateTemplate")]
    public async Task<ResponseDto> UpdateInvitation(UpdateTemplateRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UpdateTemplateRequest");
            throw;
        }
    }

    [HttpGet]
    [Route("list")]
    public async Task<TemplateDto> GetTemplateLists(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetTemplateListRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }



    [HttpGet]
    [Route("getTemplateDetails")]
    public async Task<TemplateDto> GetTemplateDetails(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetTemplateDetailsRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    [HttpPost]
    [Route("deleteTemplate")]
    public async Task<ResponseDto> DeleteTemplate(Guid id)
    {

        try
        {
            return await Mediator.Send(new DeleteTemplateRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }
}
