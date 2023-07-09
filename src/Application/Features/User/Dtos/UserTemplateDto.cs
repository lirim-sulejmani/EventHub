using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Domain.Entities;

namespace Carmax.Application.Features.User.Dtos;
public class UserTemplateDto : ResponseDto
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int StatusId { get; set; }

}
