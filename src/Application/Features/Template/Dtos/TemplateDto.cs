using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;

namespace Carmax.Application.Features.Template.Dtos;
public class TemplateDto : ResponseDto
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int StatusId { get; set; }

    
}
