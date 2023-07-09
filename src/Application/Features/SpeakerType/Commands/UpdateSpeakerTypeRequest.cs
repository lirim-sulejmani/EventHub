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
using Carmax.Application.Features.SpeakerType.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.SpeakerType.Commands;
public class UpdateSpeakerTypeRequest : IRequest<ResponseDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public class UpdateSpeakerTypeRequestHandler : IRequestHandler<UpdateSpeakerTypeRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSpeakerTypeRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(UpdateSpeakerTypeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var speakerType = _context.SpeakerTypes.FirstOrDefault(x => x.Id == request.Id);
                if (speakerType == null)
                {
                    return await Task.FromResult(new SpeakerDto { Success = false, Message = "No speaker type found with the provided Id" });
                }

                speakerType.Id = request.Id;
                speakerType.Name = request.Name;
                

                _context.SpeakerTypes.Update(speakerType);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new SpeakerTypeDto { Success = true, Message = "Speaker type is successfully updated." });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new SpeakerTypeDto { Success = false, Message = ex.Message });
            }
        }
    }
}