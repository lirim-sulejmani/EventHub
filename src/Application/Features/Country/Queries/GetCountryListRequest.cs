using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.Country.Dtos;
using Carmax.Application.Features.EventHub.Dtos;
using Carmax.Application.Features.EventHub.Queries;
using MediatR;

namespace Carmax.Application.Features.Country.Queries;
public class GetCountryListRequest : IRequest<CountryListResponseDto>
{
    public class GetCountryListRequestHandler : IRequestHandler<GetCountryListRequest, CountryListResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public GetCountryListRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CountryListResponseDto> Handle(GetCountryListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var countries = _context.Countries.Select(x => new CountryDto()
                {
                    Id = x.Id,
                    CountryName = x.CountryName,
                    CountryCode = x.CountryCode,
                    Continent = x.Continent,
                    Capital = x.Capital,
                }).ToList();

                var response = new CountryListResponseDto()
                {
                    Countries = countries,
                    Success = true
                };

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new CountryListResponseDto { Success = false, Message = ex.Message });
            }
        }
    }
}
