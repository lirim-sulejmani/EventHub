using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Country.Dtos;
using Carmax.Application.Features.EventHub.Commands;
using Carmax.Application.Features.EventHub.Dtos;
using MediatR;

namespace Carmax.Application.Features.Country.Commands;
public class UpdateCountryRequest : IRequest<ResponseDto>
{
    public Guid Id { get; set; }
    public string CountryName { get; set; }
    public int CountryCode { get; set; }
    public string Continent { get; set; }
    public string Capital { get; set; }
    public class UpdateCountryRequestHandler : IRequestHandler<UpdateCountryRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCountryRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(UpdateCountryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var countries = _context.Countries.FirstOrDefault(x => x.Id == request.Id);
                if (countries == null)
                {
                    return await Task.FromResult(new CountryDto { Success = false, Message = "No country found with the provided Id" });
                }

                countries.Id = request.Id;
                countries.CountryName = request.CountryName;
                countries.CountryCode = request.CountryCode;
                countries.Continent = request.Continent;
                countries.Capital = request.Capital;

                _context.Countries.Update(countries);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new CountryDto { Success = true, Message = "Country is successfully updated." });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new CountryDto { Success = false, Message = ex.Message });
            }
        }
    }
}


