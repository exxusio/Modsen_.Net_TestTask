using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
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
                throw new NotFoundException($"Not found with id: {request.EventId}", nameof(Event));
            }

            return _mapper.Map<EventReadDto>(_event);
        }
    }
}