using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.Invitations.Dtos;
using MediatR;

namespace Carmax.Application.Features.Invitations.Commands;
public class DeleteInvitationRequest : IRequest<ResponseDto>
{
    public Guid Id { get; private set; }
    public DeleteInvitationRequest(Guid id)
    {
        Id = id;
    }
    public class DeleteInvitationRequestHandler : IRequestHandler<DeleteInvitationRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        public DeleteInvitationRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(DeleteInvitationRequest request, CancellationToken cancellationToken)
        {

            var message = _context.Invitations.FirstOrDefault(x => x.Id == request.Id);
            if (message != null)
            {
                _context.Invitations.Remove(message);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new InvitationDto { Success = true, Message = "Invitation deleted succesfully!" });
            }
            return await Task.FromResult(new InvitationDto { Success = false, Message = "Error! No invitation found with the provided ID!" });
        }

    }
}

