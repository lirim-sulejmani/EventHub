using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.AgendaType.Dtos;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.EventHub.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.AgendaType.Commands;
public class AddAgendaTypeRequest : IRequest<ResponseDto>
{
    public string Title { get; set; }
    public Guid? SpeakerId { get; set; }
    public string BreakType { get; set; }


    public class AddAgendaTypeRequestHandler : IRequestHandler<AddAgendaTypeRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;


        public AddAgendaTypeRequestHandler(IApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ResponseDto> Handle(AddAgendaTypeRequest request, CancellationToken cancellationToken)
        {


            var entity = new Domain.Entities.AgendaType()
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                SpeakerId = request.SpeakerId,
                BreakType = request.BreakType,
               
            };
            _context.AgendaTypes.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(new AgendaTypeDto { Success = true, Message = "Agenda Type is successfully created." });

        }

    }
}

