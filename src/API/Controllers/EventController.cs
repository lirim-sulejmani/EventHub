using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using Carmax.API.Controllers;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.Event.Queries;
using Carmax.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using static System.Net.Mime.MediaTypeNames;
using Carmax.Application.Features.User.Commands;
using MediatR;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.Template.Dtos;
using Carmax.Application.Features.Template.Queries;
using Carmax.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Carmax.Application.Features.Country.Dtos;

public class EventController : ApiControllerBase
{
    private readonly IConfiguration _config;
    private readonly ApplicationDbContext _context;

    public EventController(ApplicationDbContext context, IConfiguration config)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }


    [HttpPost]
    [Route("newEvent")]
    [AllowAnonymous]
    public async Task<ActionResult> AddEvent(AddEventRequest request)
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
    [Route("updateEvent")]
    public async Task<ResponseDto> UpdateEvent(UpdateEventRequest request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UpdateEventRequest");
            throw;
        }
    }

    [HttpGet]
    [Route("list")]
    public async Task<EventDto> GetEventLists(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetEventListRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }



    [HttpGet]
    [Route("getEventDetails")]
    public async Task<EventDto> GetEventDetails(Guid id)
    {

        try
        {
            return await Mediator.Send(new GetEventDetailsRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    [HttpPost]
    [Route("deleteEvent")]
    public async Task<ResponseDto> DeleteEvent(Guid id)
    {

        try
        {
            return await Mediator.Send(new DeleteEventRequest(id));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    [HttpGet]
    [Route("SchedulerEvent")]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetSchedulerEvents([FromQuery] DateTime start, [FromQuery] DateTime end)
    {

        try
        { 

        var events = await _context.Events
            .Where(e => !((e.EndTime <= start) || (e.StartTime >= end)))
            .ToListAsync();


            if (events.Count == 0)
            {
                return NotFound();
            }
            // Convert the 'events' list to 'EventDto' list using a mapping method or constructor
            var eventDtos = events.Select(e => new EventDto
        {
            Id = e.Id,
            EventName = e.EventName,
            StartTime = e.StartTime,
            EndTime = e.EndTime,
            StatusId = e.StatusId,
            EventManager = e.EventManager,
            Organizer = e.Organizer,
            CreatedBy = e.CreatedBy,
            CreatedOn = DateTime.Now,
            Address = e.Address,
            City = e.City,
            CountryId = e.CountryId,
            ZipCode = e.ZipCode,
            EventVenue = e.EventVenue,
        }).ToList();

        return await Task.FromResult(eventDtos);
    }
        catch (Exception ex)
        {
            // Handle any exception that occurred during the query or mapping
            return StatusCode(500, "An error occurred while retrieving the events.");
        }
    }
}






