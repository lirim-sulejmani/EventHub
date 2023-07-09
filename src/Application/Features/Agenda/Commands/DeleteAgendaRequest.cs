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
using MediatR;

namespace Carmax.Application.Features.Agenda.Commands;
public class DeleteAgendaRequest : IRequest<ResponseDto>
{
    public Guid Id { get; private set; }
    public DeleteAgendaRequest(Guid id)
    {
        Id = id;
    }
    public class DeleteAgendaRequestHandler : IRequestHandler<DeleteAgendaRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        public DeleteAgendaRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(DeleteAgendaRequest request, CancellationToken cancellationToken)
        {

            var message = _context.Agendas.FirstOrDefault(x => x.Id == request.Id);
            if (message != null)
            {
                _context.Agendas.Remove(message);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new AgendaDto { Success = true, Message = "Agenda deleted succesfuly!" });
            }
            return await Task.FromResult(new AgendaDto { Success = false, Message = "Agenda! No event found with the provided ID!" });
        }

    }
}

