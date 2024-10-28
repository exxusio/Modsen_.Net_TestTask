using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Domain.Interfaces.Repositories;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Queries.GetAllEvents
{
    public class GetAllEventsHandler(
        IEventRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetAllEventsQuery, IEnumerable<EventReadDto>>
    {
        public async Task<IEnumerable<EventReadDto>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EventReadDto>>(events);
        }
    }
}