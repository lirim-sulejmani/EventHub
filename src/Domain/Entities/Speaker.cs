using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carmax.Domain.Entities;
public class Speaker
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Organization { get; set; }
    public string? Position { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImage { get; set; }
    public string? Bio { get; set; }
    public string? WebsiteUrl { get; set; }
    public Guid SocialMediaId { get; set; }
    public Guid EventId { get; set; }
    public Guid SpeakerTypeId { get;set; }
    public virtual Event Events { get; set; }
    public virtual SocialMedia SocilaMedias { get; set; }
    public virtual SpeakerType SpeakerTypes { get; set; }
    public virtual ICollection<Agenda> Agenda { get; set; }
    public virtual ICollection<AgendasSpeaker> AgendasSpeaker { get; set; }
    public virtual ICollection<AgendaType> AgendaTypes { get; set; }

}
