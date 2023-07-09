using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.EventHub.Dtos;
using Carmax.Application.Features.Invitations.Dtos;
using Carmax.Application.Features.Template.Commands;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.Invitations.Commands;
public class AddInvitationRequest : IRequest<ResponseDto>
{
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


    public class AddInvitationRequestHandler : IRequestHandler<AddInvitationRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;


        public AddInvitationRequestHandler(IApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ResponseDto> Handle(AddInvitationRequest request, CancellationToken cancellationToken)
        {



            var entity = new Domain.Entities.Invitation()
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Job = request.Job,
                Institution=request.Institution,
                NominatedBy = request.NominatedBy,
                Vip = request.Vip,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                StatusId = request.StatusId,
                QRCode = request.QRCode,
                NoGuests = request.NoGuests,
                GeneratedCode = request.GeneratedCode,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.Now,
                TemplateId=request.TemplateId,
                SendEmailError=request.SendEmailError,
                BarcodeScanned=request.BarcodeScanned,
                DateScanned=request.DateScanned,
            };
            _context.Invitations.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(new InvitationDto { Success = true, Message = "Invitation is successfully created." });

        }

    }
}

