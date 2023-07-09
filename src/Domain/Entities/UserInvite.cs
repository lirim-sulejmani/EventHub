using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carmax.Domain.Entities;
public class UserInvite
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public int StatusId { get; set; }
    public string Email { get; set; }
    public virtual User User { get; set; }
}
