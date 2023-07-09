using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.Tokens.Commands;
using Carmax.Application.Common.Exceptions;
using Carmax.Application.Features.User.Commands;
using Carmax.Application.Features.User.Dtos;
using Serilog;

namespace Carmax.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TokensController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration config;

    public TokensController(IMediator mediator, ITokenService tokenService, IConfiguration configuration)
    {
        _mediator = mediator;
        _tokenService = tokenService;
        config = configuration;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("getToken")]
    public async Task<IActionResult> GetTokenAsync(UserLoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var tokenResponse = await Mediator.Send(request);
            return Ok(tokenResponse);
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw new Exception(ex.Message);
        }
    }


    [HttpPost]
    [AllowAnonymous]
    [Route("refresh")]
    public async Task<IActionResult> RefreshAsync(RefreshTokenRequest request)
    {
        try
        {
            var tokenResponse = await _tokenService.UserRefreshTokenAsync(request);
            return Ok(tokenResponse);
        }
        catch (UnauthorizedException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("getUserToken")]
    public async Task<IActionResult> GetClientTokenAsync(UserLoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var tokenResponse = await Mediator.Send(request);
            return Ok(tokenResponse);
        }
        catch (Exception e)
        {
            Log.Error(e, e.Message);
            throw new Exception(e.Message);
        }
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("getUserRefreshToken")]
    public async Task<IActionResult> GetClientRefreshTokenAsync(RefreshTokenRequest request)
    {
        try
        {
            var tokenResponse = await _tokenService.RefreshTokenAsync(request);
            return Ok(tokenResponse);
        }
        catch (UnauthorizedException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
    }

    [HttpGet]
    [Route("getCurrentUser")]
    public async Task<UserDto> GetCurrentlyLoggedInUser()
    {
        try
        {
            return await _tokenService.GetCurrentlyLoggedInUser();
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

}