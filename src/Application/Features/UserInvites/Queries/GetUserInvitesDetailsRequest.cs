using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.UserInvites.Queries;
using Carmax.Application.Features.UserInvites.Dtos;
using MediatR;

namespace Carmax.Application.Features.UserInvites.Queries;
public class GetUserInvitesDetailsRequest : IRequest<UserInvitesDto>
{
    public Guid Id { get; private set; }
    public GetUserInvitesDetailsRequest(Guid id)
    {
        Id = id;
    }
    public class GetUserInvitesDetailsRequestHandler : IRequestHandler<GetUserInvitesDetailsRequest, UserInvitesDto>
    {
        private readonly IApplicationDbContext _context;
        public GetUserInvitesDetailsRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserInvitesDto> Handle(GetUserInvitesDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userInvent = _context.UserInvites.FirstOrDefault(x => x.Id == request.Id);
                if (userInvent != null)
                {
                    var response = new UserInvitesDto()
                    {
                        Id = userInvent.Id,
                        UserId = userInvent.UserId,
                        CreatedOn = DateTime.Now,
                        StatusId = userInvent.StatusId,
                        Email = userInvent.Email,
                    };
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(new UserInvitesDto { Success = false, Message = "Error! No user invites found with the provided ID!" });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new UserInvitesDto { Success = false, Message = ex.Message });
            }
        }
    }
}