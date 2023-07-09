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
public class AddCountryRequest : IRequest<ResponseDto>
{
    public string CountryName { get; set; }
    public int CountryCode { get; set; }
    public string Continent { get; set; }
    public string Capital { get; set; }
    public class AddCountryRequestHandler : IRequestHandler<AddCountryRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;


        public AddCountryRequestHandler(IApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ResponseDto> Handle(AddCountryRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var entity = new Domain.Entities.Country()
                {
                    Id = Guid.NewGuid(),
                    CountryName = request.CountryName,
                    CountryCode = request.CountryCode,
                    Continent = request.Continent,
                    Capital = request.Capital,
                };
                _context.Countries.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new CountryDto { Success = true, Message = "Country is successfully created." });

            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}

