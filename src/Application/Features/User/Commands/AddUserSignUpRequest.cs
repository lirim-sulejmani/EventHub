using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.User.Dtos;
using Carmax.Domain.Enums;
using Carmax.Domain.Helpers;
using MediatR;

namespace Carmax.Application.Features.User.Commands;
internal class AddUserSignUpRequest : IRequest<UserDto>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? TenantName { get; set; }
    //public string RejectComment { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public class AddUserSignUpRequestHandler : IRequestHandler<AddUserSignUpRequest, UserDto>
    {
        private readonly IApplicationDbContext _context;

        public AddUserSignUpRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserDto> Handle(AddUserSignUpRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                {
                    return await Task.FromResult(new UserDto { Message = "Email or password cannot be empty!" });
                }
                var user = _context.Users.FirstOrDefault(x => x.Email.ToLower() == request.Email.ToLower());

                if (user != null)
                    return await Task.FromResult(new UserDto { Message = "An user with this email address already exists!" });

                byte[] passwordHash;
                byte[] saltHash;
                var tenant = _context.Tenants.FirstOrDefault(x => x.Name == request.TenantName);
                HashHelper.CreatePasswordHash(request.Password, out passwordHash, out saltHash);
                var entity = new Domain.Entities.User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Address = request.Address,
                    TenantName = tenant.Name,
                    Password = passwordHash,
                    ConfirmPassword = passwordHash,
                   

                };

                _context.Users.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return new UserDto { Success = true, Message = "User is successfully created." };
            }
            catch (Exception ex)
            {
                // Include inner exception details in the response message
                var errorMessage = "An error occurred while saving the entity changes. ";
                if (ex.InnerException != null)
                    errorMessage += "Inner Exception: " + ex.InnerException.Message;
                else
                    errorMessage += "Exception: " + ex.Message;

                return new UserDto { Success = false, Message = errorMessage };
            }
        }
    }
}