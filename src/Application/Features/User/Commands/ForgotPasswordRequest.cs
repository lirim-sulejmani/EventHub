using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using MediatR;

namespace Carmax.Application.Features.User.Commands;
public class ForgotPasswordRequest : IRequest<ResponseDto>
{
    public string Email { get; set; }
    public ForgotPasswordRequest(string email)
    {
        Email = email;
    }
    public class ForgotPasswordRequestHandler : IRequestHandler<ForgotPasswordRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IEmailService _emailService;
        public ForgotPasswordRequestHandler(IApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public async Task<ResponseDto> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return new ResponseDto { Message = "Email cannot be empty" };
            }

            var user = _context.Users.FirstOrDefault(x => x.Email.ToLower() == request.Email.ToLower());
            if (user == null)
                return new ResponseDto { Message = "No user was found with this email" };

            string token = RandomString(50);
            user.ForgotPasswordToken = token;
            user.ForgotPaswordTokenExpire = DateTime.Now.AddMinutes(10);
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            var res = _emailService.SendResetPasswordEmail(new EmailTemplateModel() { Id = Guid.NewGuid(), FirstName = user.FirstName, Email = request.Email, Token = token, IsUser = false });
            return await Task.FromResult(new ResponseDto { Success = true, Message = "Email is Valid!" });
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}


