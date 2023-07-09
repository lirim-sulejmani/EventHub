using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.EventHub.Dtos;
using Carmax.Application.Features.Invitations.Commands;
using Carmax.Application.Features.UserInvites.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.UserInvites.Commands;
public class AddUserInvitesRequest : IRequest<ResponseDto>
{
    public Guid UserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public int StatusId { get; set; }
    public string Email { get; set; }


    public class AddUserInvitesRequestHandler : IRequestHandler<AddUserInvitesRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;


        public AddUserInvitesRequestHandler(IApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ResponseDto> Handle(AddUserInvitesRequest request, CancellationToken cancellationToken)
        {



            var entity = new Domain.Entities.UserInvite()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                CreatedOn = DateTime.Now,
                StatusId = request.StatusId,
                Email = request.Email,
            };
            _context.UserInvites.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(new UserInvitesDto { Success = true, Message = "User invite is successfully created." });

        }

    }
}
