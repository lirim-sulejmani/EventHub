using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Domain.Enums;

namespace Carmax.Application.Features.SpeakerType.Dtos;
public class SpeakerTypeDto : ResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
  
}
