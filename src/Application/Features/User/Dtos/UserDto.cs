using Carmax.Application.Common.Models;
using Carmax.Domain.Enums;

namespace Carmax.Application.Features.User.Dtos;
public class UserDto : ResponseDto
{

    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public DateTime CreatedOn { get; set; }
    public StatusEnum StatusId { get; set; }
    public string? RefreshToken { get; set; }
    //public string? ForgotPasswordToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    //public DateTime? ForgotPaswordTokenExpire { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public byte[] Salt { get; set; }
    public int? MaxOpenBids { get; set; }
    public string PhoneNumber { get; set; }
    public string? UserId { get; set; }
    public string? TenantName { get; set; }

    public DateTime? ForgotPaswordTokenExpire { get; set; }
    //public string? ForgotPasswordToken { get; set; }

    public UserRole? RoleId { get; set; }
    public string RejectComment { get; set; }
    public string ForgotPasswordToken { get; set; }
}



