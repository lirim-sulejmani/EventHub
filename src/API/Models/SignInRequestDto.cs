using System.ComponentModel.DataAnnotations;
using Carmax.Application.Common.Models;

namespace API.Models;

public class SignInRequestDto : ResponseDto
{
    [Required]
    public string Username { get; set; }

    public string Password { get; set; }

    public string? ReturnUrl { get; set; }

}