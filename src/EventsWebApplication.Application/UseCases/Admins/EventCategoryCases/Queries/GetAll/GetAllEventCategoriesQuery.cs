using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Queries.GetAll
{
    public class GetAllEventCategoriesQuery : IRequest<IEnumerable<EventCategoryReadDto>>
    {
    }
}