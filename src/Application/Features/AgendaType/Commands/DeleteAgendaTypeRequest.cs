using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.AgendaType.Dtos;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.Event.Dtos;
using MediatR;

namespace Carmax.Application.Features.AgendaType.Commands;
public class DeleteAgendaTypeRequest : IRequest<ResponseDto>
{
    public Guid Id { get; private set; }
    public DeleteAgendaTypeRequest(Guid id)
    {
        Id = id;
    }
    public class DeleteAgendaTypeRequestHandler : IRequestHandler<DeleteAgendaTypeRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        public DeleteAgendaTypeRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(DeleteAgendaTypeRequest request, CancellationToken cancellationToken)
        {

            var message = _context.AgendaTypes.FirstOrDefault(x => x.Id == request.Id);
            if (message != null)
            {
                _context.AgendaTypes.Remove(message);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new AgendaTypeDto { Success = true, Message = "Agenda deleted succesfuly!" });
            }
            return await Task.FromResult(new AgendaTypeDto { Success = false, Message = "Error! No agenda found with the provided ID!" });
        }

    }
}

