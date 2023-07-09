using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carmax.Domain.Entities;
public class Invitation
{

    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Job { get; set; }
    public string Institution { get; set; }
    public string NominatedBy { get; set; }
    public bool Vip { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? Website { get; set; }
    public ConfirmationStatus StatusId { get; set; }
    public string QRCode { get; set; }
    public int? NoGuests { get; set; }
    public string? GeneratedCode { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public Guid? TemplateId { get; set; }
    public string SendEmailError { get; set; }
    public bool BarcodeScanned { get; set; }
    public DateTime? DateScanned { get; set;}

    public virtual User User { get; set; }

    public virtual Template Template { get; set; }
}
