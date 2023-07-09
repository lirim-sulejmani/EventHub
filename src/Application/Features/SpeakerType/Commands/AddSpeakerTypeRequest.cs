using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.Event.Commands;
using Carmax.Application.Features.EventHub.Dtos;
using Carmax.Application.Features.SpeakerType.Dtos;
using Carmax.Domain.Enums;
using MediatR;

namespace Carmax.Application.Features.SpeakerType.Commands;
public class AddSpeakerTypeRequest : IRequest<ResponseDto>
{
    public string Name { get; set; }


    public class AddSpeakerTypeRequestHandler : IRequestHandler<AddSpeakerTypeRequest, ResponseDto>
    {
        private readonly IApplicationDbContext _context;


        public AddSpeakerTypeRequestHandler(IApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ResponseDto> Handle(AddSpeakerTypeRequest request, CancellationToken cancellationToken)
        {


            var entity = new Domain.Entities.SpeakerType()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                
            };
            _context.SpeakerTypes.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(new SpeakerTypeDto { Success = true, Message = "Speaker Type is successfully created." });

        }

    }
}

