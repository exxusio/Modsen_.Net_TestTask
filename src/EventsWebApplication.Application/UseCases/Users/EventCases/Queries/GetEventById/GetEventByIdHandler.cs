using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventById
{
    public class GetEventByIdHandler(
        IEventRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetEventByIdQuery, EventDetailedReadDto>
    {
        public async Task<EventDetailedReadDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var _event = await _repository.GetByIdAsync(request.EventId, cancellationToken);
            if (_event == null)
            {
                throw new NotFoundException($"Not found with id: {request.EventId}", nameof(_event));
            }

            return _mapper.Map<EventDetailedReadDto>(_event);
        }
    }
}