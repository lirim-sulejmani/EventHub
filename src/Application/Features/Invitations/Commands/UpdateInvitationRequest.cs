using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Invitations.Dtos;
using Carmax.Domain.Entities;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.Invitations.Commands;
public class UpdateInvitationRequest : IRequest<ResponseDto>
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
    public DateTime? DateScanned { get; set; }
    public class UpdateInvitationRequestHandler : IRequestHandler<UpdateInvitationRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateInvitationRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(UpdateInvitationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var invitation= _context.Invitations.FirstOrDefault(x => x.Id == request.Id);
                if (invitation == null)
                {
                    return await Task.FromResult(new InvitationDto { Success = false, Message = "No invitation found with the provided Id" });
                }

                invitation.Id = request.Id;
                invitation.FullName = request.FullName;
                invitation.Job = request.Job;
                invitation.Institution = request.Institution;
                invitation.NominatedBy = request.NominatedBy;
                invitation.Vip = request.Vip;
                invitation.Email = request.Email;
                invitation.PhoneNumber = request.PhoneNumber;
                invitation.StatusId = request.StatusId;
                invitation.QRCode = request.QRCode;
                invitation.NoGuests = request.NoGuests;
                invitation.GeneratedCode = request.GeneratedCode;
                invitation.CreatedBy = request.CreatedBy;
                invitation.CreatedOn = DateTime.Now;
                invitation.TemplateId = request.TemplateId;
                invitation.SendEmailError = request.SendEmailError;
                invitation.BarcodeScanned = request.BarcodeScanned;
                invitation.DateScanned = request.DateScanned;

                _context.Invitations.Update(invitation);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new InvitationDto { Success = true, Message = "Invitation is successfully updated." });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new InvitationDto { Success = false, Message = ex.Message });
            }
        }
    }
}