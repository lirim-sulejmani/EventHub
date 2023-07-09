using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.AgendaType.Dtos;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.Event.Dtos;
using Carmax.Domain.Entities;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.AgendaType.Commands;
public class UpdateAgendaTypeRequest : IRequest<ResponseDto>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid? SpeakerId { get; set; }
    public string BreakType { get; set; }
    public class UpdateAgendaTypeRequestHandler : IRequestHandler<UpdateAgendaTypeRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAgendaTypeRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Handle(UpdateAgendaTypeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var agendatype = _context.AgendaTypes.FirstOrDefault(x => x.Id == request.Id);
                if (agendatype == null)
                {
                    return await Task.FromResult(new AgendaTypeDto { Success = false, Message = "No agenda type found with the provided Id" });
                }

                agendatype.Id = request.Id;
                agendatype.Title = request.Title;
                agendatype.SpeakerId = request.SpeakerId;
                agendatype.BreakType = request.BreakType;

                _context.AgendaTypes.Update(agendatype);
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new AgendaTypeDto { Success = true, Message = "Agenda type is successfully updated." });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new AgendaTypeDto { Success = false, Message = ex.Message });
            }
        }
    }
}