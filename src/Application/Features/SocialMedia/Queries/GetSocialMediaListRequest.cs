using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.SocialMedia.Dtos;
using MediatR;

namespace Carmax.Application.Features.SocialMedia.Queries;
public class GetSocialMediaListRequest : IRequest<SocialMediaDto>
{
    public Guid Id { get; private set; }
    public GetSocialMediaListRequest(Guid id)
    {
        Id = id;
    }
    public class GetSocialMediaListRequestHandler : IRequestHandler<GetSocialMediaListRequest, SocialMediaDto>
    {
        private readonly IApplicationDbContext _context;
        public GetSocialMediaListRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SocialMediaDto> Handle(GetSocialMediaListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var socialMedia = _context.SocialMedias.FirstOrDefault(x => x.Id == request.Id);
                if (socialMedia != null)
                {
                    var response = new SocialMediaDto()
                    {
                        Id = socialMedia.Id,
                        Name = socialMedia.Name,
                        Website = socialMedia.Website,

                    };
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(new SocialMediaDto { Success = false, Message = "Error! No social media found with the provided ID!" });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new SocialMediaDto { Success = false, Message = ex.Message });
            }
        }
    }
}
