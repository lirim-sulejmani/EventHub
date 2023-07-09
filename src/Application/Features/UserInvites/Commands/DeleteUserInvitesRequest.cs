using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.UserInvites.Dtos;
using MediatR;

namespace Carmax.Application.Features.UserInvites.Commands;
public class DeleteUserInvitesRequest : IRequest<ResponseDto>
{
    public Guid Id { get; private set; }
    public DeleteUserInvitesRequest(Guid id)
    {
        Id = id;
    }
    public class DeleteUserInvitesRequestHandler : IRequestHandler<DeleteUserInvitesRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        public DeleteUserInvitesRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(DeleteUserInvitesRequest request, CancellationToken cancellationToken)
        {

            var message = _context.UserInvites.FirstOrDefault(x => x.Id == request.Id);
            if (message != null)
            {
                _context.UserInvites.Remove(message);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new UserInvitesDto { Success = true, Message = "User Invites deleted succesfully!" });
            }
            return await Task.FromResult(new UserInvitesDto { Success = false, Message = "Error! No user invites found with the provided ID!" });
        }

    }
}

