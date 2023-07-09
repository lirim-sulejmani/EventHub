using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Template.Dtos;

namespace Carmax.Application.Features.Invitations.Dtos;
public class InvitationListResponseDto : ResponseDto
{
    public List<InvitationDto> Invitations { get; set; }
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}