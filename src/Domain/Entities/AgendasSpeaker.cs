using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carmax.Domain.Entities;
public class AgendasSpeaker
{
    public Guid Id { get; set; }
    public Guid SpeakerId { get; set; }
    public Guid AgendaId { get; set; }

    public virtual Speaker Speakers { get; set; }
    public virtual Agenda Agenda { get; set; }

}
