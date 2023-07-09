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
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.Speaker.Commands;
public class UpdateSpeakerRequest : IRequest<ResponseDto>
{
    public Guid Id { get; set; }
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
    public class UpdateSpeakerRequestHandler : IRequestHandler<UpdateSpeakerRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSpeakerRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(UpdateSpeakerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var speaker = _context.Speakers.FirstOrDefault(x => x.Id == request.Id);
                if (speaker == null)
                {
                    return await Task.FromResult(new SpeakerDto { Success = false, Message = "No sp found with the provided Id" });
                }

                speaker.Id = request.Id;
                speaker.FirstName = request.FirstName;
                speaker.LastName = request.LastName;
                speaker.Email = request.Email;
                speaker.Organization = request.Organization;
                speaker.Position = request.Position;
                speaker.PhoneNumber = request.PhoneNumber;
                speaker.ProfileImage = request.ProfileImage;
                speaker.Bio = request.Bio;
                speaker.WebsiteUrl = request.WebsiteUrl;
               

                _context.Speakers.Update(speaker);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new SpeakerDto { Success = true, Message = "Speaker is successfully updated." });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new SpeakerDto { Success = false, Message = ex.Message });
            }
        }
    }
}