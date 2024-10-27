using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Helpers;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetParticipantsByEventId
{
    public class GetParticipantsByEventIdHandler(
        IEventRegistrationRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetParticipantsByEventIdQuery, IEnumerable<UserReadDto>>
    {
        public async Task<IEnumerable<UserReadDto>> Handle(GetParticipantsByEventIdQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<EventRegistration>();
            predicate = predicate.And(registration => registration.EventId == request.EventId);

            var registrations = await _repository.GetByPredicateAsync(predicate, cancellationToken);

            var users = registrations.Select(registration => registration.Participant);

            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }
    }
}