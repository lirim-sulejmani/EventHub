using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.User.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.User.Queries
{
    public class GetUsersListRequest : IRequest<UserDto>
    {
        private int _id;

        public Guid Id { get; }

        public GetUsersListRequest(Guid id)
        {
            Id = id;
        }

        public GetUsersListRequest(int id)
        {
            _id = id;
        }

        public class GetUsersListRequestHandler : IRequestHandler<GetUsersListRequest, UserDto>
        {
            private readonly IApplicationDbContext _context;

            public GetUsersListRequestHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<UserDto> Handle(GetUsersListRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(x => x.Id == request.Id);

                    if (user == null)
                    {
                        return new UserDto { Success = false, Message = "User not found." };
                    }

                    var userDto = new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Address = user.Address,
                        CreatedOn = user.CreatedOn,
                        RoleId = (UserRole)user.RoleId,
                        City = user.City,
                        //CreatedBy = (Guid)user.CreatedBy,
                        //RefreshToken = user.RefreshToken,
                        //RefreshTokenExpiryTime = (DateTime)user.RefreshTokenExpiryTime,
                        StatusId = StatusEnum.StatusId,
                        Email = user.Email,

                    };

                    return await Task.FromResult(userDto);
                }
                catch (Exception ex)
                {
                    return new UserDto { Success = false, Message = ex.Message };
                }
            }
        }
    }
}
