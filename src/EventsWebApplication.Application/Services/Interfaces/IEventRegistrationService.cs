using EventsWebApplication.Application.DTOs.EventRegistrations;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Services.Interfaces
{
    public interface IEventRegistrationService : IService<EventRegistration, EventRegistrationReadDto, EventRegistrationDetailedReadDto, EventRegistrationCreateDto, EventRegistrationUpdateDto>
    {

    }
}