using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.SpeakerType.Dtos;
using MediatR;

namespace Carmax.Application.Features.SpeakerType.Commands;
public class DeleteSpeakerTypeRequest : IRequest<ResponseDto>
{
    public Guid Id { get; private set; }
    public DeleteSpeakerTypeRequest(Guid id)
    {
        Id = id;
    }
    public class DeleteSpeakerTypeRequestHandler : IRequestHandler<DeleteSpeakerTypeRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        public DeleteSpeakerTypeRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(DeleteSpeakerTypeRequest request, CancellationToken cancellationToken)
        {

            var message = _context.SpeakerTypes.FirstOrDefault(x => x.Id == request.Id);
            if (message != null)
            {
                _context.SpeakerTypes.Remove(message);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new SpeakerTypeDto { Success = true, Message = "Speaker type deleted succesfuly!" });
            }
            return await Task.FromResult(new SpeakerTypeDto { Success = false, Message = "Error! No speaker type found with the provided ID!" });
        }

    }
}


