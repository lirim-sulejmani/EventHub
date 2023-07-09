using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;

namespace Carmax.Application.Features.AgendaType.Dtos;
public class AgendaTypeDto : ResponseDto
{

    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid? SpeakerId { get; set; }
    public string BreakType { get; set; }
}
