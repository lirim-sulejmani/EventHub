using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.EventHub.Dtos;
using MediatR;

namespace Carmax.Application.Features.EventHub.Commands;
public class DeleteTenantRequest : IRequest<ResponseDto>
{
    public Guid Id { get; private set; }
    public DeleteTenantRequest(Guid id)
    {
        Id = id;
    }
    public class DeleteTenantRequestHandler : IRequestHandler<DeleteTenantRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        public DeleteTenantRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(DeleteTenantRequest request, CancellationToken cancellationToken)
        {

            var message = _context.Tenants.FirstOrDefault(x => x.Id == request.Id);
            if (message != null)
            {
                _context.Tenants.Remove(message);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new TenantDto { Success = true, Message = "EventHub deleted succesfully!" });
            }
            return await Task.FromResult(new TenantDto { Success = false, Message = "Error! No eventhub found with the provided ID!" });
        }

    }
}

