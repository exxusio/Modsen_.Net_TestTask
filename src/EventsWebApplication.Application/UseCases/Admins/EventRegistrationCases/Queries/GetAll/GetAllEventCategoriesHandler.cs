using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;

namespace EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetAll
{
    public class GetAllEventRegistrationsHandler(
        IEventRegistrationRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetAllEventRegistrationsQuery, IEnumerable<EventRegistrationReadDto>>
    {
        public async Task<IEnumerable<EventRegistrationReadDto>> Handle(GetAllEventRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var registrations = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EventRegistrationReadDto>>(registrations);
        }
    }
}