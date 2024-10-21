using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Services.Interfaces
{
    public interface IEventService : IService<Event, EventReadDto, EventDetailedReadDto, EventCreateDto, EventUpdateDto>
    {

    }
}