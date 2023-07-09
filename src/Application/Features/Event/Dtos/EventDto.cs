using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Domain.Enums;

namespace Carmax.Application.Features.Event.Dtos;
public class EventDto : ResponseDto
{
    public Guid Id { get; set; }
    public string EventName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public EventStatus StatusId { get; set; }
    public string EventManager { get; set; }
    public string Organizer { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public Guid CountryId { get; set; }
    public int ZipCode { get; set; }
    public string EventVenue { get; set; }

}
