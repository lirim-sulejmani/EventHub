using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.UserInvites.Dtos;

namespace Carmax.Application.Features.UserInvites.Dtos;
public class UserInvitesListResponseDto : ResponseDto
{
    public List<UserInvitesDto> UserInvites { get; set; }
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}