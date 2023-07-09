using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;

namespace Carmax.Application.Features.EventHub.Dtos;
public class TenantDto : ResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int StatusId { get; set; }
    public DateTime CreatedOn { get; set; }
}
