using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Repositories;

namespace EventsWebApplication.Application.UseCases.Users.EventCategoryCases.Queries.GetAllCategories
{
    public class GetAllCategoriesHandler(
        IEventCategoryRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<EventCategoryReadDto>>
    {
        public async Task<IEnumerable<EventCategoryReadDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EventCategoryReadDto>>(categories);
        }
    }
}