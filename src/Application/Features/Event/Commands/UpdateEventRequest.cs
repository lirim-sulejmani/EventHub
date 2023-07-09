using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.Event.Commands;
using Carmax.Domain.Enums;
using MediatR;
using Carmax.Application.Features.EventHub.Commands;
using Carmax.Application.Features.EventHub.Dtos;
using Microsoft.Extensions.Logging;

namespace Carmax.Application.Features.Event.Commands;
public class UpdateEventRequest : IRequest<ResponseDto>
{
    public Guid Id { get; set; }
    public string EventName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public EventStatus StatusId { get; set; }
    public string EventManager { get; set; }
    public string Organizer { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public Guid CountryId { get; set; }
    public int ZipCode { get; set; }
    public string EventVenue { get; set; }
    public class UpdateEventRequestHandler : IRequestHandler<UpdateEventRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEventRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(UpdateEventRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var events = _context.Events.FirstOrDefault(x => x.Id == request.Id);
                if (events == null)
                {
                    return await Task.FromResult(new EventDto { Success = false, Message = "No event found with the provided Id" });
                }

                events.Id = request.Id;
                events.EventName = request.EventName;
                events.StartTime = request.StartTime;
                events.EndTime = request.EndTime;
                events.StatusId = request.StatusId;
                events.EventManager = request.EventManager;
                events.Organizer = request.Organizer;
                events.CreatedBy = request.CreatedBy;
                events.CreatedOn = DateTime.Now;
                events.Address = request.Address;
                events.City = request.City;
                events.CountryId = request.CountryId;
                events.ZipCode = request.ZipCode;
                events.EventVenue = request.EventVenue;

                _context.Events.Update(events);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new EventDto { Success = true, Message = "Event is successfully updated." });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new EventDto { Success = false, Message = ex.Message });
            }
        }
    }
}