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
using MediatR;

namespace Carmax.Application.Features.Template.Commands;
public class AddTemplateRequest : IRequest<ResponseDto>
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int StatusId { get; set; }

    public class AddTemplateRequestHandler : IRequestHandler<AddTemplateRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;


        public AddTemplateRequestHandler(IApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ResponseDto> Handle(AddTemplateRequest request, CancellationToken cancellationToken)
        {


            var entity = new Domain.Entities.Template()
            {
                Id = Guid.NewGuid(),
                Subject = request.Subject,
                Body = request.Body,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.Now,
                StatusId = request.StatusId,
            };
            _context.Templates.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(new TemplateDto { Success = true, Message = "Template is successfully created." });

        }

    }
}

