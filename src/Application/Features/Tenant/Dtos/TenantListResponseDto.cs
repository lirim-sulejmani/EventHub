using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.EventHub.Dtos;

namespace Carmax.Application.Features.EventHub.Dtos;
public class TenantListResponseDto : ResponseDto
{
    public List<TenantDto> Tenants { get; set; }
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
