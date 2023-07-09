using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carmax.Domain.Entities;
public class AgendaType
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid? SpeakerId { get; set; }
    public string BreakType { get; set; }

    public virtual Speaker Speakers { get; set; }
    public virtual ICollection<Agenda> Agenda { get; set; }

}
