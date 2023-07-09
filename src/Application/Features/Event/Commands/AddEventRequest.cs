using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.EventHub.Commands;
using Carmax.Application.Features.EventHub.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.Event.Commands;
public class AddEventRequest : IRequest<ResponseDto>
{
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





    public class AddEventRequestHandler : IRequestHandler<AddEventRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;


        public AddEventRequestHandler(IApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ResponseDto> Handle(AddEventRequest request, CancellationToken cancellationToken)
        {
            

            var entity = new Domain.Entities.Event()
            {
                Id =Guid.NewGuid(),
                EventName = request.EventName,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                StatusId = request.StatusId,
                EventManager=request.EventManager,
                Organizer=request.Organizer,
                CreatedBy=request.CreatedBy,
                CreatedOn = DateTime.Now,
                Address=request.Address,
                City=request.City,
                CountryId=request.CountryId,
                ZipCode=request.ZipCode,    
                EventVenue=request.EventVenue,
            };
            _context.Events.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(new TenantDto { Success = true, Message = "Event is successfully created." });

        }

    }
}

