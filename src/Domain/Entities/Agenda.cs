using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carmax.Domain.Entities;
public class Agenda
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
    public virtual User Users { get; set; }
    public virtual AgendaType AgendaTypes { get; set; }
    public virtual Speaker Speakers { get; set; }
    public virtual Event Events { get; set; }
    public virtual ICollection<AgendasSpeaker> AgendasSpeakers { get; set; }



}
