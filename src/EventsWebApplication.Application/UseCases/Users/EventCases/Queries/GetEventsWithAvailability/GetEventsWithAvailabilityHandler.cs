using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Domain.Interfaces.Repositories;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventsWithAvailability
{
    public class GetEventsWithAvailabilityHandler(
        IEventRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetEventsWithAvailabilityQuery, IEnumerable<EventWithAvailabilityDto>>
    {
        public async Task<IEnumerable<EventWithAvailabilityDto>> Handle(GetEventsWithAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var events = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EventWithAvailabilityDto>>(events);
        }
    }
}