using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Invitations.Commands;
using Carmax.Application.Features.Invitations.Dtos;
using Carmax.Application.Features.UserInvites.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.UserInvites.Commands;
public class UpdateUserInvitesRequest : IRequest<ResponseDto>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public int StatusId { get; set; }
    public string Email { get; set; }
    public class UpdateUserInvitesRequestHandler : IRequestHandler<UpdateUserInvitesRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserInvitesRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(UpdateUserInvitesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userinvites = _context.UserInvites.FirstOrDefault(x => x.Id == request.Id);
                if (userinvites == null)
                {
                    return await Task.FromResult(new InvitationDto { Success = false, Message = "No user invites found with the provided Id" });
                }

                userinvites.Id = request.Id;
                userinvites.UserId = request.UserId;
                userinvites.CreatedOn = DateTime.Now;
                userinvites.StatusId = request.StatusId;
                userinvites.Email = request.Email;

                _context.UserInvites.Update(userinvites);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new UserInvitesDto { Success = true, Message = "User invites is successfully updated." });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new UserInvitesDto { Success = false, Message = ex.Message });
            }
        }
    }
}
