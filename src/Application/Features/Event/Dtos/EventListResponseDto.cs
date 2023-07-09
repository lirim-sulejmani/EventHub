using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Event.Dtos;

namespace Carmax.Application.Features.Event.Dtos;
public class EventListResponseDto : ResponseDto
{
    public List<EventDto> Events { get; set; }
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
