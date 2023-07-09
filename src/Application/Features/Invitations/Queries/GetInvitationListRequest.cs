using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Application.Features.Event.Queries;
using Carmax.Application.Features.Invitations.Dtos;
using Carmax.Domain.Entities;
using MediatR;

namespace Carmax.Application.Features.Invitations.Queries;
public class GetInvitationListRequest : IRequest<InvitationDto>
{
    public Guid Id { get; private set; }
    public GetInvitationListRequest(Guid TenantId)
    {
        Id = TenantId;
    }
    public class GetInvitationListRequestHandler : IRequestHandler<GetInvitationListRequest, InvitationDto>
    {
        private readonly IApplicationDbContext _context;
        public GetInvitationListRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<InvitationDto> Handle(GetInvitationListRequest request, CancellationToken cancellationToken)
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
