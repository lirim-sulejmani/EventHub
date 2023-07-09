using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using Carmax.API.Controllers;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.EventHub.Commands;
using Carmax.Application.Features.EventHub.Dtos;
using Carmax.Application.Features.EventHub.Queries;
using Carmax.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using static System.Net.Mime.MediaTypeNames;
using Carmax.Application.Features.User.Commands;
using MediatR;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TenantController : ApiControllerBase
{
    public readonly IConfiguration _config;

    public TenantController(IConfiguration config)
    {
        _config = config;
    }
    [HttpPost]
    [Route("newTenant")]
    [AllowAnonymous]
    public async Task<ActionResult> AddTenant(AddTenantRequest request)
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
    [Route("updateTenant")]
    public async Task<ResponseDto> UpdateTenant(UpdateTenantRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UpdateTenantRequest");
            throw;
        }
    }

    [HttpGet]
    [Route("list")]
    public async Task<TenantListResponseDto> GetTenants()
    {
        try
        {
            return await Mediator.Send(new GetTenantListRequest());
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }



    [HttpGet]
    [Route("getTenantsDetails")]
    public async Task<TenantDto> GetTenantsDetails(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetTenantDetailsRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    [HttpPost]
    [Route("deleteTenant")]
    public async Task<ResponseDto> DeleteTenant(Guid id)
    {

        try
        {
            return await Mediator.Send(new DeleteTenantRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }
}
