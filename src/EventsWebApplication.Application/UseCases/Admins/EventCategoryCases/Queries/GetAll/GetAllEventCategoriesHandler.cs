using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;

namespace EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Queries.GetAll
{
    public class GetAllEventCategoriesHandler(
        IEventCategoryRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetAllEventCategoriesQuery, IEnumerable<EventCategoryReadDto>>
    {
        public async Task<IEnumerable<EventCategoryReadDto>> Handle(GetAllEventCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EventCategoryReadDto>>(categories);
        }
    }
}