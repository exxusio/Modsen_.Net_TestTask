using EventsWebApplication.Application.DTOs.EventCategories;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Services.Interfaces
{
    public interface IEventCategoryService : IService<EventCategory, EventCategoryReadDto, EventCategoryDetailedReadDto, EventCategoryCreateDto, EventCategoryUpdateDto>
    {
    }
}