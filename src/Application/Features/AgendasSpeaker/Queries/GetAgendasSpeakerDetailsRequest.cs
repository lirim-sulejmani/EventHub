using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.AgendasSpeaker.Dtos;
using MediatR;

namespace Carmax.Application.Features.AgendasSpeaker.Queries;
public class GetAgendasSpeakerDetailsRequest : IRequest<AgendasSpeakerDto>
{
    public Guid Id { get; private set; }
    public GetAgendasSpeakerDetailsRequest(Guid id)
    {
        Id = id;
    }
    public class GetAgendasSpeakerRequestHandler : IRequestHandler<GetAgendasSpeakerDetailsRequest, AgendasSpeakerDto>
    {
        private readonly IApplicationDbContext _context;
        public GetAgendasSpeakerRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AgendasSpeakerDto> Handle(GetAgendasSpeakerDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var agendaspeaker = _context.AgendasSpeakers.FirstOrDefault(x => x.Id == request.Id);
                if (agendaspeaker != null)
                {
                    var response = new AgendasSpeakerDto()
                    {
                        Id = agendaspeaker.Id,
                        SpeakerId = agendaspeaker.SpeakerId,
                        AgendaId = agendaspeaker.AgendaId,

                    };
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(new AgendasSpeakerDto { Success = false, Message = "Error! No speaker agend found with the provided ID!" });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new AgendasSpeakerDto { Success = false, Message = ex.Message });
            }
        }
    }
}
