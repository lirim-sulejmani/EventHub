using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Domain.Entities;
using Carmax.Domain.Enums;

namespace Carmax.Application.Features.Agenda.Dtos;
public class AgendaDto : ResponseDto
{

    public Guid Id { get; set; }

    public Guid AgendaTypeId { get; set; }
    public Guid EventId { get; set; }
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string? Room { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? SpeakerId { get; set; }
    public AgendaStatus StatusId { get; set; }
  



}
