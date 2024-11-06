using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.DeleteCategory
{
    public class DeleteCategoryCommand
    : IRequest<EventCategoryReadDto>
    {
        [BindNever]
        public Guid CategoryId { get; set; }
    }
}