using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Agenda.Dtos;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.Agenda.Commands;
public class UpdateAgendaRequest : IRequest<ResponseDto>
{
    public Guid Id { get; set; }    
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

    public class UpdateAgendaRequestHandler : IRequestHandler<UpdateAgendaRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAgendaRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(UpdateAgendaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var agenda = _context.Agendas.FirstOrDefault(x => x.Id == request.Id);
                if (agenda == null)
                {
                    return await Task.FromResult(new AgendaDto { Success = false, Message = "No agenda found with the provided Id" });
                }

                agenda.Id = request.Id;
                agenda.AgendaTypeId = request.AgendaTypeId;
                agenda.EventId = request.EventId;
                agenda.StartTime = request.StartTime;
                agenda.EndTime = request.EndTime;
                agenda.Room = request.Room;
                agenda.CreatedBy = request.CreatedBy;
                agenda.CreatedAt = request.CreatedAt;
                agenda.UpdatedAt = request.UpdatedAt;
                agenda.SpeakerId = request.SpeakerId;
                agenda.StatusId = request.StatusId;
                
                _context.Agendas.Update(agenda);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new AgendaDto { Success = true, Message = "Agenda is successfully updated." });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new AgendaDto { Success = false, Message = ex.Message });
            }
        }
    }
}