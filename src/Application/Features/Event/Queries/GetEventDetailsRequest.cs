using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.Event.Queries;
using MediatR;

namespace Carmax.Application.Features.Event.Queries;
public class GetEventDetailsRequest : IRequest<EventDto>
{
    public Guid Id { get; private set; }
    public GetEventDetailsRequest(Guid id)
    {
        Id = id;
    }
    public class GetEventDetailsRequestHandler : IRequestHandler<GetEventDetailsRequest, EventDto>
    {
        private readonly IApplicationDbContext _context;
        public GetEventDetailsRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<EventDto> Handle(GetEventDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var events = _context.Events.FirstOrDefault(x => x.Id == request.Id);
                if (events != null)
                {
                    var response = new EventDto()
                    {
                        Id = events.Id,
                        EventName = events.EventName,
                        StartTime = events.StartTime,
                        EndTime = events.EndTime,
                        StatusId = events.StatusId,
                        EventManager = events.EventManager,
                        Organizer = events.Organizer,
                        CreatedBy = events.CreatedBy,
                        CreatedOn = DateTime.Now,
                        Address = events.Address,
                        City = events.City,
                        CountryId = events.CountryId,
                        ZipCode = events.ZipCode,
                        EventVenue = events.EventVenue,
                    };
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(new EventDto { Success = false, Message = "Error! No event found with the provided ID!" });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new EventDto { Success = false, Message = ex.Message });
            }
        }
    }
}
