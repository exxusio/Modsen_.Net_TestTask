using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;

namespace EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetEventRegistrations
{
    public class GetEventRegistrationsHandler(
        IEventRegistrationRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetEventRegistrationsQuery, IEnumerable<EventRegistrationReadDto>>
    {
        public async Task<IEnumerable<EventRegistrationReadDto>> Handle(GetEventRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var registrations = await _repository.GetRegistrationsByEventIdAsync(request.EventId, cancellationToken);

            return _mapper.Map<IEnumerable<EventRegistrationReadDto>>(registrations);
        }
    }
}