using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Agenda.Dtos;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.EventHub.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.Agenda.Commands;
public class AddAgendaRequest : IRequest<ResponseDto>
{
    public Guid AgendaTypeId { get; set; }
    public Guid EventId { get; set; }
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string? Room { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? SpeakerId { get; set; }
    public AgendaStatus StatusId { get; set; }





    public class AddAgendaRequestHandler : IRequestHandler<AddAgendaRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;


        public AddAgendaRequestHandler(IApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ResponseDto> Handle(AddAgendaRequest request, CancellationToken cancellationToken)
        {


            var entity = new Domain.Entities.Agenda()
            {
                Id = Guid.NewGuid(),
                AgendaTypeId = request.AgendaTypeId,
                EventId = request.EventId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Room = request.Room,
                CreatedBy = request.CreatedBy,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt,
                SpeakerId = request.SpeakerId,
                StatusId = AgendaStatus.Completed,
                
            };
            _context.Agendas.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(new AgendaDto { Success = true, Message = "Agenda is successfully created." });

        }

    }
}

