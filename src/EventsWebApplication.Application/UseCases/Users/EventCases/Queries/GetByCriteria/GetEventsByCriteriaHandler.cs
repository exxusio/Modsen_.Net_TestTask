using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Application.Helpers;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetByCriteria
{
    public class GetEventsByCriteriaHandler(
        IEventRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetEventsByCriteriaQuery, IEnumerable<EventReadDto>>
    {
        public async Task<IEnumerable<EventReadDto>> Handle(GetEventsByCriteriaQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<Event>();

            if (request.Date.HasValue)
            {
                predicate = predicate.And(_event => _event.Date == request.Date.Value);
            }

            if (!string.IsNullOrEmpty(request.Location))
            {
                predicate = predicate.And(_event => _event.Location.Contains(request.Location));
            }

            if (request.CategoryId != null)
            {
                predicate = predicate.And(_event => _event.CategoryId == request.CategoryId);
            }

            var events = await _repository.GetByPredicateAsync(predicate, cancellationToken);
            return _mapper.Map<IEnumerable<EventReadDto>>(events);
        }
    }
}