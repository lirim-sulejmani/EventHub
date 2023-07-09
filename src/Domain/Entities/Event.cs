using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Carmax.Domain.Entities;
public class Event
{
 
    public Guid Id { get; set; }

    public string EventName { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public EventStatus StatusId { get; set; }

    public string? EventManager { get; set; }
    public string? Organizer { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string EventVenue { get; set; }

    public string City { get; set; }
    public string Address { get; set; }
    public Guid CountryId { get; set; }
    public int ZipCode{get;set;}

    public virtual Country Country { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<Speaker> Speakers { get; set; }
    public virtual ICollection<Agenda> Agenda { get; set; }
}
