using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.Event.Queries;
using Carmax.Application.Features.Speaker.Dtos;
using MediatR;

namespace Carmax.Application.Features.Speaker.Queries;
public class GetSpeakerDetailsRequest : IRequest<SpeakerDto>
{
    public Guid Id { get; private set; }
    public GetSpeakerDetailsRequest(Guid id)
    {
        Id = id;
    }
    public class GetSpeakerDetailsRequestHandler : IRequestHandler<GetSpeakerDetailsRequest, SpeakerDto>
    {
        private readonly IApplicationDbContext _context;
        public GetSpeakerDetailsRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SpeakerDto> Handle(GetSpeakerDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var speaker = _context.Speakers.FirstOrDefault(x => x.Id == request.Id);
                if (speaker != null)
                {
                    var response = new SpeakerDto()
                    {
                        Id= speaker.Id,
                        FirstName = speaker.FirstName,
                        LastName = speaker.LastName,
                        Email = speaker.Email,
                        Organization = speaker.Organization,
                        Position = speaker.Position,
                        PhoneNumber = speaker.PhoneNumber,
                        ProfileImage = speaker.ProfileImage,
                        Bio = speaker.Bio,
                        WebsiteUrl = speaker.WebsiteUrl,
                        SocialMediaId = speaker.SocialMediaId,
                        EventId = speaker.EventId,
                        SpeakerTypeId = speaker.SpeakerTypeId,
                    };
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(new SpeakerDto { Success = false, Message = "Error! No speaker found with the provided ID!" });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new SpeakerDto { Success = false, Message = ex.Message });
            }
        }
    }
}
