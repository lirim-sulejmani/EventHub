using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.EventHub.Dtos;

namespace Carmax.Application.Features.Template.Dtos;
public class TemplateListResponseDto : ResponseDto
{
    public List<TemplateDto> Templates { get; set; }
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
