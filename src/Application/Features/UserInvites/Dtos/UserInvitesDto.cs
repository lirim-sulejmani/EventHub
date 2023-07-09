using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;

namespace Carmax.Application.Features.UserInvites.Dtos;
public class UserInvitesDto : ResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public int StatusId { get; set; }
    public string Email { get; set; }
   
}
