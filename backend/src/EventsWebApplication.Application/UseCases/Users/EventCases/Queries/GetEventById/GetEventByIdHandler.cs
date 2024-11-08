using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Abstractions.Caching;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventById
{
    public class GetEventByIdHandler(
        ICacheService _cache,
        IEventRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetEventByIdQuery, EventReadDto>
    {
        public async Task<EventReadDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var cachedEvent = await _cache.GetAsync<EventReadDto>(request.EventId.ToString());
            if (cachedEvent != null)
            {
                return cachedEvent;
            }

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

            var eventReadDto = _mapper.Map<EventReadDto>(_event);

            await _cache.SetAsync(eventReadDto.Id.ToString(), eventReadDto);

            return eventReadDto;
        }
    }
}