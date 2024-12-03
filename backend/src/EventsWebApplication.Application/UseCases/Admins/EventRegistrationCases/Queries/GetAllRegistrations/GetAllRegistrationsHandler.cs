using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;

namespace EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetAllRegistrations
{
    public class GetAllRegistrationsHandler(
        IEventRegistrationRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetAllRegistrationsQuery, IEnumerable<EventRegistrationReadDto>>
    {
        public async Task<IEnumerable<EventRegistrationReadDto>> Handle(GetAllRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var registrations = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EventRegistrationReadDto>>(registrations);
        }
    }
}