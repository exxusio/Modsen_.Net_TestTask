using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Application.Helpers;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetByCriteria
{
    public class GetByCriteriaEventHandler(
        IEventRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetByCriteriaEventQuery, IEnumerable<EventReadDto>>
    {
        public async Task<IEnumerable<EventReadDto>> Handle(GetByCriteriaEventQuery request, CancellationToken cancellationToken)
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

            // var events = await _repository.GetByPredicateAsync(
            //     eventDto =>
            //         (!request.Date.HasValue || eventDto.Date == request.Date.Value) &&
            //         (string.IsNullOrEmpty(request.Location) || eventDto.Location.Contains(request.Location)) &&
            //         (request.CategoryId == null || eventDto.CategoryId == request.CategoryId),
            //     cancellationToken
            //     );

            var events = await _repository.GetByPredicateAsync(predicate, cancellationToken);
            return _mapper.Map<IEnumerable<EventReadDto>>(events);
        }
    }
}