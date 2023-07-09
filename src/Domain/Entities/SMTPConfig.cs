using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carmax.Domain.Entities;
public class SMTPConfig
{
    public Guid Id { get; set; }

    public string SMTPHost { get; set; }

    public int SMTPPort { get; set; }
    public string SMTPAuthentication { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsTLS { get; set; }
    public string MessageFrom { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }
}
