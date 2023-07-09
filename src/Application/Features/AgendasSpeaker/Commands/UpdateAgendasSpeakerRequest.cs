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
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.AgendasSpeaker.Commands;
public class UpdateAgendasSpeakerRequest : IRequest<ResponseDto>
{
    public Guid Id { get; set; }
    public Guid SpeakerId { get; set; }
    public Guid AgendaId { get; set; }
    public class UpdateAgendasSpeakerRequestHandler : IRequestHandler<UpdateAgendasSpeakerRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAgendasSpeakerRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(UpdateAgendasSpeakerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var agendasSpeaker = _context.AgendasSpeakers.FirstOrDefault(x => x.Id == request.Id);
                if (agendasSpeaker == null)
                {
                    return await Task.FromResult(new AgendasSpeakerDto { Success = false, Message = "No speaker agenda found with the provided Id" });
                }

                agendasSpeaker.Id = request.Id;
                agendasSpeaker.SpeakerId = request.SpeakerId;
                agendasSpeaker.AgendaId = request.AgendaId;
               

                _context.AgendasSpeakers.Update(agendasSpeaker);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new AgendasSpeakerDto { Success = true, Message = "Agenda's speaker is successfully updated." });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new AgendasSpeakerDto { Success = false, Message = ex.Message });
            }
        }
    }
}