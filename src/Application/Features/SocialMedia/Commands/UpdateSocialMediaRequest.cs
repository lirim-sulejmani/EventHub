using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.SocialMedia.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.SocialMedia.Commands;
public class UpdateSocialMediaRequest : IRequest<ResponseDto>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Website { get; set; }
    public class UpdateSocialMediaRequestHandler : IRequestHandler<UpdateSocialMediaRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSocialMediaRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(UpdateSocialMediaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var socialMedia = _context.SocialMedias.FirstOrDefault(x => x.Id == request.Id);
                if (socialMedia == null)
                {
                    return await Task.FromResult(new SocialMediaDto { Success = false, Message = "No social media found with the provided Id" });
                }

                socialMedia.Id = request.Id;
                socialMedia.Name = request.Name;
                socialMedia.Website = request.Website;
                

                _context.SocialMedias.Update(socialMedia);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new SocialMediaDto { Success = true, Message = "Social media is successfully updated." });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new SocialMediaDto { Success = false, Message = ex.Message });
            }
        }
    }
}