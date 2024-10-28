using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<EventCategoryReadDto>
    {
        public Guid Id { get; set; }
    }
}