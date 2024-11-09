using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Users.EventCategoryCases.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery
    : IRequest<IEnumerable<EventCategoryReadDto>>
    {
    }
}