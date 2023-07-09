using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carmax.Domain.Entities;
public class Tenant
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int StatusId { get; set; }

    public DateTime CreatedOn { get; set; }
    public virtual ICollection<Invitation> Invitations { get; set;}
    public virtual ICollection<Event> Events { get; set; }
    public virtual ICollection<Template> Templates { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<UserInvite> UserInvite { get; set; }



}
