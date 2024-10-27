using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.Create
{
    public class CreateEventCategoryCommand : IRequest<EventCategoryReadDto>
    {
        public string Name { get; set; }
    }
}