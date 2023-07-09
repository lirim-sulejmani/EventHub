using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.Country.Dtos;
using MediatR;

namespace Carmax.Application.Features.Country.Queries;
public class GetCountryDetailsRequest : IRequest<CountryDto>
{
    public Guid Id { get; private set; }
    public GetCountryDetailsRequest(Guid id)
    {
        Id = id;
    }
    public class GetEventHubDetailsRequestHandler : IRequestHandler<GetCountryDetailsRequest, CountryDto>
    {
        private readonly IApplicationDbContext _context;
        public GetEventHubDetailsRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CountryDto> Handle(GetCountryDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var eventhubs = _context.Countries.FirstOrDefault(x => x.Id == request.Id);
                if (eventhubs != null)
                {
                    var response = new CountryDto()
                    {
                        Id = eventhubs.Id,
                        CountryName = eventhubs.CountryName,
                        CountryCode = eventhubs.CountryCode,
                        Continent = eventhubs.Continent,
                        Capital = eventhubs.Capital,
                        Success = true,
                    };
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(new CountryDto { Success = false, Message = "Error! No country found with the provided ID!" });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new CountryDto { Success = false, Message = ex.Message });
            }
        }
    }
}
