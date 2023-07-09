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
using MediatR;

namespace Carmax.Application.Features.SocialMedia.Commands;
public class DeleteSocialMediaRequest : IRequest<ResponseDto>
{
    public Guid Id { get; private set; }
    public DeleteSocialMediaRequest(Guid id)
    {
        Id = id;
    }
    public class DeleteSocialMediaRequestHandler : IRequestHandler<DeleteSocialMediaRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        public DeleteSocialMediaRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(DeleteSocialMediaRequest request, CancellationToken cancellationToken)
        {

            var message = _context.SocialMedias.FirstOrDefault(x => x.Id == request.Id);
            if (message != null)
            {
                _context.SocialMedias.Remove(message);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new SocialMediaDto { Success = true, Message = "Social media deleted succesfuly!" });
            }
            return await Task.FromResult(new SocialMediaDto { Success = false, Message = "Error! No social media found with the provided ID!" });
        }

    }
}
