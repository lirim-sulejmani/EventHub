using Carmax.Domain.Entities;
using Carmax.Domain.Enums;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
namespace Carmax.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public DateTime CreatedOn { get; set; } 
    public Guid? CreatedBy { get; set; }
    public StatusEnum StatusId { get; set; }
    public string? RefreshToken { get; set; }
    //public string? ForgotPasswordToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    //public DateTime? ForgotPaswordTokenExpire { get; set; }
    public UserRole? RoleId { get; set; }
    public Guid TenantId { get; set; }
    public virtual Tenant Tenant { get; set; }
    public virtual ICollection<Event> Events { get; set; }
    public virtual ICollection<Invitation> Invitations { get; set; }
    public virtual ICollection<Template> Templates { get; set; }
    public virtual ICollection<UserInvite> UserInvites { get; set; }
    public DateTime ForgotPaswordTokenExpire { get;  set; }
    public string ForgotPasswordToken { get;  set; }
}
