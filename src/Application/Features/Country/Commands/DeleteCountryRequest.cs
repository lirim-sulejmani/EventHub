using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Country.Dtos;
using MediatR;

namespace Carmax.Application.Features.Country.Commands;
public class DeleteCountryRequest : IRequest<ResponseDto>
{
    public Guid Id { get; private set; }
    public DeleteCountryRequest(Guid id)
    {
        Id = id;
    }
    public class DeleteCountryRequestHandler : IRequestHandler<DeleteCountryRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;
        public DeleteCountryRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(DeleteCountryRequest request, CancellationToken cancellationToken)
        {

            var message = _context.Countries.FirstOrDefault(x => x.Id == request.Id);
            if (message != null)
            {
                _context.Countries.Remove(message);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new CountryDto { Success = true, Message = "Country deleted succesfully!" });
            }
            return await Task.FromResult(new CountryDto { Success = false, Message = "Error! No country found with the provided ID!" });
        }

    }
}

