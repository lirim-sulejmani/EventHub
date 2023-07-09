using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.EventHub.Dtos;
using MediatR;

namespace Carmax.Application.Features.EventHub.Queries;
public class GetTenantDetailsRequest : IRequest<TenantDto>
{
    public Guid Id { get; private set; }
    public GetTenantDetailsRequest(Guid id)
    {
        Id = id;
    }
    public class GetTenantDetailsRequestHandler : IRequestHandler<GetTenantDetailsRequest, TenantDto>
    {
        private readonly IApplicationDbContext _context;
        public GetTenantDetailsRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TenantDto> Handle(GetTenantDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tenants = _context.Tenants.FirstOrDefault(x => x.Id ==request.Id);
                if (tenants != null)
                {
                    var response = new TenantDto()
                    {
                        Id = tenants.Id,
                        Name = tenants.Name,
                        StatusId = tenants.StatusId,
                        CreatedOn = tenants.CreatedOn,
                        Success = true,
                    };
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(new TenantDto { Success = false, Message = "Error! No eventhub found with the provided ID!" });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new TenantDto { Success = false, Message = ex.Message });
            }
        }
    }
}
