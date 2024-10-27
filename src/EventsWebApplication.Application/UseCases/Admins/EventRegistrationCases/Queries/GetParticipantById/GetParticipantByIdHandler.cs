using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Helpers;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Exceptions;

namespace EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetParticipantById
{
    public class GetParticipantByIdHandler(
        IEventRegistrationRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetParticipantByIdQuery, UserReadDto>
    {
        public async Task<UserReadDto> Handle(GetParticipantByIdQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<EventRegistration>();
            predicate = predicate.And(registration => registration.ParticipantId == request.UserId);
            predicate = predicate.And(registration => registration.EventId == request.EventId);

            var registration = (await _repository.GetByPredicateAsync(predicate, cancellationToken)).FirstOrDefault();

            if (registration == null)
            {
                throw new NotFoundException($"Not found with id: {request.UserId}", nameof(User));
            }

            return _mapper.Map<UserReadDto>(registration.Participant);
        }
    }
}