using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using MediatR;

namespace Carmax.Application.Features.User.Queries;
public class CheckIfEmailExistsRequest : IRequest<ResponseDto>
{
    public string Email { get; set; }
    public CheckIfEmailExistsRequest(string email)
    {
        Email = email;
    }
    public class CheckIfEmailExistsRequestHandler : IRequestHandler<CheckIfEmailExistsRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        public CheckIfEmailExistsRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(CheckIfEmailExistsRequest request, CancellationToken cancellationToken)
        {
            var emailExists = _context.Users.Any(x => x.Email == request.Email);
            if (emailExists)
            {
                return await Task.FromResult(new ResponseDto { Success = false, Message = "This email is taken, try another one!" });
            }
            return await Task.FromResult(new ResponseDto { Success = true, Message = "Email is Valid!" });
        }
    }
}
