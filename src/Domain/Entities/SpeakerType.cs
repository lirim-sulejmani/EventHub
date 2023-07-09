using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carmax.Domain.Entities;
public class SpeakerType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
   
    public virtual ICollection<Speaker> Speakers { get; set; }
}
