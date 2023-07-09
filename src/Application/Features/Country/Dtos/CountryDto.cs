using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;

namespace Carmax.Application.Features.Country.Dtos;
public class CountryDto : ResponseDto
{
    public Guid Id { get; set; }
    public string CountryName { get; set; }
    public int CountryCode { get; set; }
    public string Continent { get; set; }

    public string Capital { get; set; }
}
