using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<EventCategoryReadDto>
    {
        public string Name { get; set; }
    }
}