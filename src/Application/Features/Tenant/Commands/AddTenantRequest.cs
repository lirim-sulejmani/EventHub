using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.EventHub.Dtos;
using Carmax.Application.Features.Template.Dtos;
using Carmax.Application.Features.User.Dtos;
using MediatR;

namespace Carmax.Application.Features.EventHub.Commands;
public class AddTenantRequest : IRequest<ResponseDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int StatusId { get; set; }
    public DateTime CreatedOn { get; set; }
    public class AddTenantRequestHandler : IRequestHandler<AddTenantRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
       

        public AddTenantRequestHandler(IApplicationDbContext context)
        {
            _context = context;
            
        }
        public async Task<ResponseDto> Handle(AddTenantRequest request, CancellationToken cancellationToken)
        {
           

                var entity = new Domain.Entities.Tenant()
                {
                    Id= request.Id,
                    Name = request.Name,
                    StatusId = request.StatusId,
                    CreatedOn = DateTime.Now,
                };
                _context.Tenants.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(new TenantDto { Success = true, Message = "EventHub is successfully created." });

        }

    }
    }

