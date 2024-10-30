using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetRegistrationDetails
{
    public class GetRegistrationDetailsHandler(
        IEventRegistrationRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetRegistrationDetailsQuery, EventRegistrationReadDto>
    {
        public async Task<EventRegistrationReadDto> Handle(GetRegistrationDetailsQuery request, CancellationToken cancellationToken)
        {
            var registration = await _repository.GetRegistrationByEventIdAndParticipantIdAsync(request.UserId, request.EventId, cancellationToken);

            if (registration == null)
            {
                throw new NotFoundException($"Not found with id: {request.UserId}", nameof(User));
            }

            return _mapper.Map<EventRegistrationReadDto>(registration);
        }
    }
}