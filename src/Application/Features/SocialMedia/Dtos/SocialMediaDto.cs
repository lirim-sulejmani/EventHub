using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Domain.Entities;

namespace Carmax.Application.Features.SocialMedia.Dtos;
public class SocialMediaDto : ResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Website { get; set; }

}
