using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Domain.Helpers;
using MediatR;

namespace Carmax.Application.Features.User.Commands;
public class ChangeUserPasswordRequest : IRequest<ResponseDto>
{
    public string CurrentPassword { get; set; }
    public string Password { get; set; }
    public string ConfirmNewPassword { get; set; }
    public class ChangeClientPasswordRequestHandler : IRequestHandler<ChangeUserPasswordRequest, ResponseDto>
{
    private readonly IApplicationDbContext _context;
    public ChangeClientPasswordRequestHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ResponseDto> Handle(ChangeUserPasswordRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == Guid.NewGuid());
            if (user != null)
            {
                    if (!HashHelper.VerifyPasswordHash(request.CurrentPassword, user.Password, user.Salt))
                {
                    throw new Exception("Incorrect Password!");
                }
                else if (request.Password != request.ConfirmNewPassword)
                {
                    throw new Exception("The Confirm Password does not match with New Password!");
                }

                byte[] passwordHash, passwordSalt;
                HashHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                    user.Password = passwordHash;
                    user.Salt = passwordSalt;
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new ResponseDto() { Success = true, Message = "Password changed succesfuly!" });
            }
            return await Task.FromResult(new ResponseDto() { Success = false, Message = "Error while trying to change password!" });
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new ResponseDto() { Success = false, Message = ex.Message });
        }
    }
}
}
