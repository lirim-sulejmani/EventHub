using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.EventHub.Dtos;
using MediatR;

namespace Carmax.Application.Features.EventHub.Queries
{
    public class GetTenantListRequest : IRequest<TenantListResponseDto>
    {
        public class GetTenantListRequestHandler : IRequestHandler<GetTenantListRequest, TenantListResponseDto>
        {
            private readonly IApplicationDbContext _context;

            public GetTenantListRequestHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<TenantListResponseDto> Handle(GetTenantListRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var eventhubs = _context.Tenants.Select(x => new TenantDto()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        StatusId = x.StatusId,
                        CreatedOn = x.CreatedOn,
                    }).ToList();

                    var response = new TenantListResponseDto()
                    {
                        Tenants = eventhubs,
                        Success = true
                    };

                    return await Task.FromResult(response);
                }
                catch (Exception ex)
                {
                    return await Task.FromResult(new TenantListResponseDto { Success = false, Message = ex.Message });
                }
            }
        }
    }
}
