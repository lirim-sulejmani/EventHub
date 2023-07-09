using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Carmax.Domain.Entities;
public class User 
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public DateTime CreatedOn { get; set; }
    public StatusEnum StatusId { get; set; }
    public string? RefreshToken { get; set; }
    //public string? ForgotPasswordToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    //public DateTime? ForgotPaswordTokenExpire { get; set; }
    public byte[] Password { get;set; }
    public byte[] ConfirmPassword { get; set; }
    public byte[] Salt { get; set; }
    public int? MaxOpenBids { get; set; }
    public string PhoneNumber { get; set; }
    public string? TenantName { get; set; }

    //public string? RejectComment { get; set; }
    public DateTime? ForgotPaswordTokenExpire { get; set; }
    public string? ForgotPasswordToken { get; set; }

    public UserRole RoleId { get; set; }
    public virtual ICollection<Event>? Events { get; set; }
    public virtual ICollection<Invitation>? Invitations { get; set; }
    public virtual ICollection<Template>? Templates { get; set; }
    public virtual ICollection<UserInvite>? UserInvite { get; set; }
    public virtual ICollection<Agenda> Agenda { get; set; }
}
