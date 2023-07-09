using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.EventHub.Commands;
using Carmax.Application.Features.EventHub.Dtos;
using Carmax.Application.Features.Template.Dtos;
using Carmax.Domain.Entities;
using MediatR;

namespace Carmax.Application.Features.Template.Commands;
public class UpdateTemplateRequest : IRequest<ResponseDto>
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int StatusId { get; set; }
    public class UpdateTemplateRequestHandler : IRequestHandler<UpdateTemplateRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTemplateRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(UpdateTemplateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var template = _context.Templates.FirstOrDefault(x => x.Id == request.Id);
                if (template == null)
                {
                    return await Task.FromResult(new TemplateDto { Success = false, Message = "No template found with the provided Id" });
                }

                template.Id = request.Id;
                template.Subject = request.Subject;
                template.Body = request.Body;
                template.CreatedBy = request.CreatedBy;
                template.CreatedOn = DateTime.Now;
                template.StatusId = request.StatusId;

                _context.Templates.Update(template);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new TemplateDto { Success = true, Message = "Template is successfully updated." });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new TemplateDto { Success = false, Message = ex.Message });
            }
        }
    }
}

