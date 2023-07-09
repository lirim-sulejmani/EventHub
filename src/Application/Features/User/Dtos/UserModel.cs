using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;

namespace Carmax.Application.Features.User.Dtos;
public class UserModel : ResponseDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

}