using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.AgendasSpeaker.Dtos;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.Event.Dtos;
using MediatR;

namespace Carmax.Application.Features.AgendasSpeaker.Commands;
public class DeleteAgendasSpeakerRequest : IRequest<ResponseDto>
{
    public Guid Id { get; private set; }
    public DeleteAgendasSpeakerRequest(Guid id)
    {
        Id = id;
    }
    public class DeleteAgendasSpeakerRequestHandler : IRequestHandler<DeleteAgendasSpeakerRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        public DeleteAgendasSpeakerRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(DeleteAgendasSpeakerRequest request, CancellationToken cancellationToken)
        {

            var message = _context.AgendasSpeakers.FirstOrDefault(x => x.Id == request.Id);
            if (message != null)
            {
                _context.AgendasSpeakers.Remove(message);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new AgendasSpeakerDto { Success = true, Message = "Agenda's Speaker deleted succesfuly!" });
            }
            return await Task.FromResult(new AgendasSpeakerDto { Success = false, Message = "Error! No agenda of speaker found with the provided ID!" });
        }

    }
}

