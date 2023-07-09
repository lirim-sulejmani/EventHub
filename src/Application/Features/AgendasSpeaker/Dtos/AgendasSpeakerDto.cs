using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;

namespace Carmax.Application.Features.AgendasSpeaker.Dtos;
public class AgendasSpeakerDto : ResponseDto
{
    public Guid Id { get; set; }
    public Guid SpeakerId { get; set; }
    public Guid AgendaId { get; set; }
}
