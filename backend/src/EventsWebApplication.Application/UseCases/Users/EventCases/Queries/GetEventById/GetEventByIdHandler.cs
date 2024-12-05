using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventById
{
    public class GetEventByIdHandler(
        IEventRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetEventByIdQuery, EventReadDto>
    {
        public async Task<EventReadDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var _event = await _repository.GetByIdAsync(request.EventId, cancellationToken);
            if (_event == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(Event),
                    nameof(request.EventId),
                    request.EventId.ToString()
                );
            }

            return _mapper.Map<EventReadDto>(_event);
        }
    }
}