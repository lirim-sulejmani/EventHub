using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Template.Commands;
using Carmax.Application.Features.Template.Dtos;
using MediatR;

namespace Carmax.Application.Features.Template.Commands;
public class DeleteTemplateRequest : IRequest<ResponseDto>
{
    public Guid Id { get; private set; }
    public DeleteTemplateRequest(Guid id)
    {
        Id = id;
    }
    public class DeleteTemplateRequestHandler : IRequestHandler<DeleteTemplateRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        public DeleteTemplateRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(DeleteTemplateRequest request, CancellationToken cancellationToken)
        {

            var template = _context.Templates.FirstOrDefault(x => x.Id == request.Id);
            if (template != null)
            {
                _context.Templates.Remove(template);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new TemplateDto { Success = true, Message = "Template deleted succesfully!" });
            }
            return await Task.FromResult(new TemplateDto { Success = false, Message = "Error! No template found with the provided ID!" });
        }

    }
}

