using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventsByFilter
{
    public class GetEventsByFilterHandler(
        IEventRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetEventsByFilterQuery, IEnumerable<EventReadDto>>
    {
        public async Task<IEnumerable<EventReadDto>> Handle(GetEventsByFilterQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<EventFilter>(request);
            var paged = _mapper.Map<PagedFilter>(request);

            var events = await _repository.GetEventsByFilterAsync(paged, filter, cancellationToken);
            return _mapper.Map<IEnumerable<EventReadDto>>(events);
        }
    }
}