using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using Carmax.API.Controllers;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Country.Commands;
using Carmax.Application.Features.Country.Dtos;
using Carmax.Application.Features.Country.Queries;
using Carmax.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using static System.Net.Mime.MediaTypeNames;
using Carmax.Application.Features.User.Commands;
using MediatR;
using Carmax.Application.Features.Event.Commands;

namespace API.Controllers;

public class CountryController : ApiControllerBase
{
    public readonly IConfiguration _config;

    public CountryController(IConfiguration config)
    {
        _config = config;
    }
    [HttpPost]
    [Route("newCountry")]
    [AllowAnonymous]
    public async Task<ActionResult> AddCountry(AddCountryRequest request)
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
    [Route("updateCountry")]
    public async Task<ResponseDto> UpdateCountry(UpdateCountryRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UpdateCountryRequest");
            throw;
        }
    }

    [HttpGet]
    [Route("list")]
    public async Task<CountryListResponseDto> GetCountries()
    {
        try
        {
            return await Mediator.Send(new GetCountryListRequest());
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }



    [HttpGet]
    [Route("getCountryDetails")]
    public async Task<CountryDto> GetCountryDetails(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetCountryDetailsRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    [HttpPost]
    [Route("deleteCountry")]
    public async Task<ResponseDto> DeleteCountry(Guid id)
    {

        try
        {
            return await Mediator.Send(new DeleteCountryRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }
}
