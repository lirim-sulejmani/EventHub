using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.Template.Dtos;
using Carmax.Application.Features.Template.Queries;
using MediatR;

namespace Carmax.Application.Features.Template.Queries;
public class GetTemplateDetailsRequest : IRequest<TemplateDto>
{
    public Guid Id { get; private set; }
    public GetTemplateDetailsRequest(Guid id)
    {
        Id = id;
    }
    public class GetTemplateDetailsRequestHandler : IRequestHandler<GetTemplateDetailsRequest, TemplateDto>
    {
        private readonly IApplicationDbContext _context;
        public GetTemplateDetailsRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TemplateDto> Handle(GetTemplateDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var templates = _context.Templates.FirstOrDefault(x => x.Id == request.Id);
                if (templates != null)
                {
                    var response = new TemplateDto()
                    {
                        Id = templates.Id,
                        Subject = templates.Subject,
                        Body = templates.Body,
                        CreatedBy = templates.CreatedBy,
                        CreatedOn = DateTime.Now,
                        StatusId = templates.StatusId,
                        Success = true,
                    };
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(new TemplateDto { Success = false, Message = "Error! No template found with the provided ID!" });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new TemplateDto { Success = false, Message = ex.Message });
            }
        }
    }
}
