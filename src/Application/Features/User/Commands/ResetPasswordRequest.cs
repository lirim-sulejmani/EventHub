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
public class ResetPasswordRequest : IRequest<ResponseDto>
{
    public string Token { get; set; }
    public string Password { get; set; }
    public string ConfirmNewPassword { get; set; }

    public class ResetPasswordRequestHandler : IRequestHandler<ResetPasswordRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        public ResetPasswordRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Password != request.ConfirmNewPassword)
                    throw new Exception("The Confirm Password does not match with New Password!");
                var user = _context.Users.FirstOrDefault(x => x.ForgotPasswordToken == request.Token);
                if (string.IsNullOrWhiteSpace(request.Token) || user == null || user.ForgotPaswordTokenExpire < DateTime.Now)
                {
                    return await Task.FromResult(new ResponseDto() { Success = false, Message = "Sesion expired!" });
                }
                byte[] passwordHash;
                byte[] saltHash;
                if (user != null)
                {
                    HashHelper.CreatePasswordHash(request.Password, out passwordHash, out saltHash);
                    user.Password = passwordHash;
                    user.Salt = saltHash;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync(cancellationToken);
                    return await Task.FromResult(new ResponseDto() { Success = true, Message = "Your password has been successfully reset!" });
                }
                return await Task.FromResult(new ResponseDto() { Success = false, Message = "Error while trying to reset password!" });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseDto() { Success = false, Message = ex.Message });
            }
        }
    }
}
