using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.AgendasSpeaker.Dtos;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.EventHub.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.AgendasSpeaker.Commands;
public class AddAgendasSpeakerRequest : IRequest<ResponseDto>
{
    public Guid SpeakerId { get; set; }
    public Guid AgendaId { get; set; }




    public class AddAgendasSpeakerRequestHandler : IRequestHandler<AddAgendasSpeakerRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;


        public AddAgendasSpeakerRequestHandler(IApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ResponseDto> Handle(AddAgendasSpeakerRequest request, CancellationToken cancellationToken)
        {


            var entity = new Domain.Entities.AgendasSpeaker()
            {
                Id = Guid.NewGuid(),
                SpeakerId = request.SpeakerId,
                AgendaId = request.AgendaId,
               
            };
            _context.AgendasSpeakers.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(new AgendasSpeakerDto { Success = true, Message = "Speaker's agenda is successfully created." });

        }

    }
}

