using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.EventHub.Dtos;
using Carmax.Application.Features.SocialMedia.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.SocialMedia.Commands;
public class AddSocialMediaRequest : IRequest<ResponseDto>
{

    public string Name { get; set; }
    public string Website { get; set; }





    public class AddSocialMediaRequestHandler : IRequestHandler<AddSocialMediaRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;


        public AddSocialMediaRequestHandler(IApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ResponseDto> Handle(AddSocialMediaRequest request, CancellationToken cancellationToken)
        {


            var entity = new Domain.Entities.SocialMedia()
            {
                Id = Guid.NewGuid(),
                Name = request.Website,
                Website = request.Website,
                
            };
            _context.SocialMedias.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(new SocialMediaDto { Success = true, Message = "Social Media is successfully created." });

        }

    }
}

