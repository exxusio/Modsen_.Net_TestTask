using MediatR;
using AutoMapper;
using EventsWebApplication.Application.Helpers;
using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Queries.GetEventsByParticipantId
{
    public class GetEventsByParticipantIdHandler(
        IEventRegistrationRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetEventsByParticipantIdQuery, IEnumerable<EventReadDto>>
    {
        public async Task<IEnumerable<EventReadDto>> Handle(GetEventsByParticipantIdQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<EventRegistration>();
            predicate = predicate.And(registration => registration.ParticipantId == request.UserId);

            var registrations = await _repository.GetByPredicateAsync(predicate, cancellationToken);

            var events = registrations.Select(registration => registration.Event);

            return _mapper.Map<IEnumerable<EventReadDto>>(events);
        }
    }
}