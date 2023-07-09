using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Country.Dtos;

namespace Carmax.Application.Features.Country.Dtos;
public class CountryListResponseDto : ResponseDto
{
    public List<CountryDto> Countries { get; set; }
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}