using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Queries.GetUserRegistrations
{
    public class GetUserRegistrationsHandler(
        IEventRegistrationRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetUserRegistrationsQuery, IEnumerable<EventRegistrationReadDto>>
    {
        public async Task<IEnumerable<EventRegistrationReadDto>> Handle(GetUserRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var registrations = await _repository.GetByParticipantIdAsync(request.UserId, cancellationToken);
            return _mapper.Map<IEnumerable<EventRegistrationReadDto>>(registrations);
        }
    }
}