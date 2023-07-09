using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Features.Agenda.Dtos;
using Carmax.Application.Features.AgendaType.Dtos;
using MediatR;

namespace Carmax.Application.Features.AgendaType.Queries;
public class GetAgendaTypeDetailsRequest : IRequest<AgendaTypeDto>
{
    public Guid Id { get; private set; }
    public GetAgendaTypeDetailsRequest(Guid id)
    {
        Id = id;
    }
    public class GetAgendaTypeDetailsRequestHandler : IRequestHandler<GetAgendaTypeDetailsRequest, AgendaTypeDto>
    {
        private readonly IApplicationDbContext _context;
        public GetAgendaTypeDetailsRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AgendaTypeDto> Handle(GetAgendaTypeDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var agendatype = _context.AgendaTypes.FirstOrDefault(x => x.Id == request.Id);
                if (agendatype != null)
                {


                    var response = new AgendaTypeDto()
                    {
                        Id = Guid.NewGuid(),
                        Title = agendatype.Title,
                        SpeakerId = agendatype.SpeakerId,
                    };
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(new AgendaTypeDto { Success = false, Message = "Error! No event found with the provided ID!" });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new AgendaTypeDto { Success = false, Message = ex.Message });
            }
        }
    }
}
