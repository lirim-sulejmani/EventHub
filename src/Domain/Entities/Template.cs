using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carmax.Domain.Entities;
public class Template
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int StatusId { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<Invitation> Invitations { get; set; }

}
