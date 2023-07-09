using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.EventHub.Dtos;
using Carmax.Application.Features.Speaker.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.Speaker.Commands;
public class AddSpeakerRequest : IRequest<ResponseDto>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Organization { get; set; }
    public string? Position { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImage { get; set; }
    public string? Bio { get; set; }
    public string? WebsiteUrl { get; set; }
    public Guid SocialMediaId { get; set; }
    public Guid EventId { get; set; }
    public Guid SpeakerTypeId { get; set; }





    public class AddSpeakerRequestHandler : IRequestHandler<AddSpeakerRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;


        public AddSpeakerRequestHandler(IApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ResponseDto> Handle(AddSpeakerRequest request, CancellationToken cancellationToken)
        {


            var entity = new Domain.Entities.Speaker()
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Organization = request.Organization,
                Position = request.Position,
                PhoneNumber = request.PhoneNumber,
                ProfileImage = request.ProfileImage,
                Bio = request.Bio,
                WebsiteUrl = request.WebsiteUrl,
                SocialMediaId = request.SocialMediaId,
                EventId = request.EventId,
                SpeakerTypeId = request.SpeakerTypeId,
            };
            _context.Speakers.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(new SpeakerDto { Success = true, Message = "Speaker is successfully created." });

        }

    }
}

