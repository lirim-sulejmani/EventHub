using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.Country.Dtos;
using Carmax.Application.Features.Country.Queries;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.Event.Queries;
using MediatR;

namespace Carmax.Application.Features.Event.Queries;

public class GetEventListRequest : IRequest<EventDto>
{
    public Guid Id { get; private set; }
    public GetEventListRequest(Guid TenantId)
    {
        Id = TenantId;
    }
    public class GetEventListRequestHandler : IRequestHandler<GetEventListRequest, EventDto>
    {
        private readonly IApplicationDbContext _context;
        public GetEventListRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<EventDto> Handle(GetEventListRequest request, CancellationToken cancellationToken)
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

