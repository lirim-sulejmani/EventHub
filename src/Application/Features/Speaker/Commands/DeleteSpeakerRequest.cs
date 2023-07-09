using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.Speaker.Dtos;
using MediatR;

namespace Carmax.Application.Features.Speaker.Commands;
public class DeleteSpeakerRequest : IRequest<ResponseDto>
{
    public Guid Id { get; private set; }
    public DeleteSpeakerRequest(Guid id)
    {
        Id = id;
    }
    public class DeleteSpeakerRequestHandler : IRequestHandler<DeleteSpeakerRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        public DeleteSpeakerRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(DeleteSpeakerRequest request, CancellationToken cancellationToken)
        {

            var message = _context.Speakers.FirstOrDefault(x => x.Id == request.Id);
            if (message != null)
            {
                _context.Speakers.Remove(message);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new SpeakerDto { Success = true, Message = "Speaker deleted succesfuly!" });
            }
            return await Task.FromResult(new SpeakerDto { Success = false, Message = "Error! No speaker found with the provided ID!" });
        }

    }
}

