using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventsByFilter
{
    public class GetEventsByFilterHandler(
        IEventRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetEventsByFilterQuery, GetEventsByFilterResponse>
    {
        public async Task<GetEventsByFilterResponse> Handle(GetEventsByFilterQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<EventFilter>(request);
            var paged = _mapper.Map<PagedFilter>(request);

            var (events, totalEvents) = await _repository.GetByFilterAsync(paged, filter, cancellationToken);

            var eventDtos = _mapper.Map<IEnumerable<EventReadDto>>(events);

            return new GetEventsByFilterResponse
            {
                Events = eventDtos,
                TotalCount = totalEvents
            };
        }
    }
}