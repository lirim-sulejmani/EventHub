using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Domain.Enums;

namespace Carmax.Application.Features.Speaker.Dtos;
public class SpeakerDto : ResponseDto
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
    public Guid SpeakerTypeId { get; set; }

}
