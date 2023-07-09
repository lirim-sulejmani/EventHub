using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.SpeakerType.Dtos;
using MediatR;

namespace Carmax.Application.Features.SpeakerType.Queries;
public class GetSpeakerTypeListRequest : IRequest<SpeakerTypeDto>
{
    public Guid Id { get; private set; }
    public GetSpeakerTypeListRequest(Guid id)
    {
        Id = id;
    }
    public class GetSpeakerTypeListRequestHandler : IRequestHandler<GetSpeakerTypeListRequest, SpeakerTypeDto>
    {
        private readonly IApplicationDbContext _context;
        public GetSpeakerTypeListRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SpeakerTypeDto> Handle(GetSpeakerTypeListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var speakerType = _context.SpeakerTypes.FirstOrDefault(x => x.Id == request.Id);
                if (speakerType != null)
                {
                    var response = new SpeakerTypeDto()
                    {
                        Id = speakerType.Id,
                        Name = speakerType.Name,
                    };
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(new SpeakerTypeDto { Success = false, Message = "Error! No speaker type found with the provided ID!" });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new SpeakerTypeDto { Success = false, Message = ex.Message });
            }
        }
    }
}
