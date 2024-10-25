using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Services.Interfaces
{
    public interface IEventService : IService<Event, EventReadDto, EventDetailedReadDto, EventCreateDto, EventUpdateDto>
    {
        Task<EventDetailedReadDto> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<EventReadDto>> GetByCriteriaAsync(EventCriteriaDto dto, CancellationToken cancellationToken = default);
        Task<IEnumerable<EventWithAvailabilityDto>> GetEventsWithAvailabilityAsync(CancellationToken cancellationToken = default);
    }
}