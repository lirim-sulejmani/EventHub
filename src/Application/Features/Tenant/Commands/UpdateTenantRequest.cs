using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.EventHub.Dtos;
using Carmax.Application.Features.User.Commands;
using Carmax.Application.Features.User.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.EventHub.Commands;
public class UpdateTenantRequest : IRequest<ResponseDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int StatusId { get; set; }
    public DateTime CreatedOn { get; set; }
    public class UpdateTenantRequestHandler : IRequestHandler<UpdateTenantRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTenantRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(UpdateTenantRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tenant = _context.Tenants.FirstOrDefault(x => x.Id == request.Id);
                if (tenant == null)
                {
                    return await Task.FromResult(new TenantDto { Success = false, Message = "No eventhub found with the provided Id" });
                }

                tenant.Id = request.Id;
                tenant.Name = request.Name;
                tenant.StatusId = request.StatusId;
                tenant.CreatedOn = request.CreatedOn;

                _context.Tenants.Update(tenant);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new TenantDto { Success = true, Message = "EventHub is successfully updated." });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new TenantDto { Success = false, Message = ex.Message });
            }
        }
    }
}

