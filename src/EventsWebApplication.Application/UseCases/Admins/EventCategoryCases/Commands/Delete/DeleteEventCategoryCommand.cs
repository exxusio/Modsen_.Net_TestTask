using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.Delete
{
    public class DeleteEventCategoryCommand : IRequest<EventCategoryReadDto>
    {
        public Guid Id { get; set; }
    }
}