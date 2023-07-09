using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.Invitations.Dtos;
using Carmax.Application.Features.Invitations.Queries;
using Carmax.Application.Features.Invitations.Dtos;
using MediatR;

namespace Carmax.Application.Features.Invitations.Queries;
public class GetInvitationDetailsRequest : IRequest<InvitationDto>
{
    public Guid Id { get; private set; }
    public GetInvitationDetailsRequest(Guid id)
    {
        Id = id;
    }
    public class GetInvitationDetailsRequestHandler : IRequestHandler<GetInvitationDetailsRequest, InvitationDto>
    {
        private readonly IApplicationDbContext _context;
        public GetInvitationDetailsRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<InvitationDto> Handle(GetInvitationDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var invitation = _context.Invitations.FirstOrDefault(x => x.Id == request.Id);
                if (invitation != null)
                {
                    var response = new InvitationDto()
                    {
                        Id = invitation.Id,
                        FullName = invitation.FullName,
                        Job = invitation.Job,
                        Institution = invitation.Institution,
                        NominatedBy = invitation.NominatedBy,
                        Vip = invitation.Vip,
                        Email = invitation.Email,
                        PhoneNumber = invitation.PhoneNumber,
                        StatusId = invitation.StatusId,
                        QRCode = invitation.QRCode,
                        NoGuests = invitation.NoGuests,
                        GeneratedCode = invitation.GeneratedCode,
                        CreatedBy = invitation.CreatedBy,
                        CreatedOn = DateTime.Now,
                        TemplateId = invitation.TemplateId,
                        SendEmailError = invitation.SendEmailError,
                        BarcodeScanned = invitation.BarcodeScanned,
                        DateScanned = invitation.DateScanned,
                    };
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(new InvitationDto { Success = false, Message = "Error! No invitation found with the provided ID!" });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new InvitationDto { Success = false, Message = ex.Message });
            }
        }
    }
}
