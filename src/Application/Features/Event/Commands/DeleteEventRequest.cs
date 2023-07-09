using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Event.Dtos;
using MediatR;


namespace Carmax.Application.Features.Event.Commands;
public class DeleteEventRequest : IRequest<ResponseDto>
{
    public Guid Id { get; private set; }
    public DeleteEventRequest(Guid id)
    {
        Id = id;
    }
    public class DeleteEventRequestHandler : IRequestHandler<DeleteEventRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        public DeleteEventRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(DeleteEventRequest request, CancellationToken cancellationToken)
        {

            var message = _context.Events.FirstOrDefault(x => x.Id == request.Id);
            if (message != null)
            {
                _context.Events.Remove(message);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new EventDto { Success = true, Message = "Event deleted succesfuly!" });
            }
            return await Task.FromResult(new EventDto { Success = false, Message = "Error! No event found with the provided ID!" });
        }

    }
}

