using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carmax.Domain.Entities;
public class SocialMedia
{
    public Guid Id { get; set; }    

    public string Name { get; set; }
    public string Website { get; set; }
    public virtual ICollection<Speaker> Speakers { get; set; }

}
