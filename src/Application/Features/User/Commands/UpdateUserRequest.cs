using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.User.Dtos;
using Carmax.Domain.Entities;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.User.Commands;
public class UpdateUserRequest : IRequest<ResponseDto>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    //public DateTime CreatedOn { get; set; }
    // public StatusEnum StatusId { get; set; }
    //public string? RefreshToken { get; set; }
    //public string? ForgotPasswordToken { get; set; }
    //public DateTime? RefreshTokenExpiryTime { get; set; }
    //public DateTime? ForgotPaswordTokenExpire { get; set; }

    //public byte[] Salt { get; set; }
    public int MaxOpenBids { get; set; }
    public string PhoneNumber { get; set; }

    public UserRole RoleId { get; set; }
    public Guid TenantId { get; set; }
    //public string RejectComment { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var users = _context.Users.FirstOrDefault(x => x.Id == request.Id);
                if (users == null)
                {
                    return await Task.FromResult(new UserDto { Success = false, Message = "No user found with the provided Id" });
                }
                users.Id = request.Id;
                users.FirstName = request.FirstName;
                users.LastName = request.LastName;
                users.Email = request.Email;
                users.Address = request.Address;
                users.CreatedOn = DateTime.Now;
                users.RoleId = request.RoleId;
                users.City = request.City;
                users.StatusId = StatusEnum.StatusId;

                users.MaxOpenBids = request.MaxOpenBids;
                users.PhoneNumber = request.PhoneNumber;
               // users. RejectComment = request.RejectComment;

                _context.Users.Update(users);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new UserDto { Success = true, Message = "User is successfully updated." });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new UserDto { Success = false, Message = ex.Message });
            }
        }
    }
}