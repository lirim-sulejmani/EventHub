using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.Agenda.Dtos;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.Event.Queries;
using MediatR;

namespace Carmax.Application.Features.Agenda.Queries;
public class GetAgendaDetailsRequest : IRequest<AgendaDto>
{
    public Guid Id { get; private set; }
    public GetAgendaDetailsRequest(Guid id)
    {
        Id = id;
    }
    public class GetAgendaDetailsRequestHandler : IRequestHandler<GetAgendaDetailsRequest, AgendaDto>
    {
        private readonly IApplicationDbContext _context;
        public GetAgendaDetailsRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AgendaDto> Handle(GetAgendaDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var agenda = _context.Agendas.FirstOrDefault(x => x.Id == request.Id);
                if (agenda != null)
                {
                    var response = new AgendaDto()
                    {

                        AgendaTypeId = agenda.AgendaTypeId,
                        EventId = agenda.EventId,
                        StartTime = agenda.StartTime,
                        EndTime = agenda.EndTime,
                        Room = agenda.Room,
                        CreatedBy = agenda.CreatedBy,
                        CreatedAt = agenda.CreatedAt,
                        UpdatedAt = agenda.UpdatedAt,
                        SpeakerId = agenda.SpeakerId,
                        StatusId = agenda.StatusId,

                    };
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(new AgendaDto { Success = false, Message = "Error! No agenda found with the provided ID!" });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new AgendaDto { Success = false, Message = ex.Message });
            }
        }
    }
}