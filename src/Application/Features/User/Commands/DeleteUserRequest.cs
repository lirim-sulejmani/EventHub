using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.User.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.User.Commands
{
    public class DeleteUserRequest : IRequest<ResponseDto>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public DeleteUserRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == request.Id);
                if (user == null)
                {
                    return await Task.FromResult(new ResponseDto { Success = false, Message = "No user found with the provided Id" });
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new ResponseDto { Success = true, Message = "User has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseDto { Success = false, Message = ex.Message });
            }
        }
    }
}
