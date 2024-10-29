using MediatR;
using AutoMapper;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Queries.GetUserRegistrations
{
    public class GetUserRegistrationsHandler(
        IEventRegistrationRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetUserRegistrationsQuery, IEnumerable<EventRegistrationReadDto>>
    {
        public async Task<IEnumerable<EventRegistrationReadDto>> Handle(GetUserRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var registrations = await _repository.GetRegistrationsByParticipantIdAsync(request.UserId, cancellationToken);

            return _mapper.Map<IEnumerable<EventRegistrationReadDto>>(registrations);
        }
    }
}