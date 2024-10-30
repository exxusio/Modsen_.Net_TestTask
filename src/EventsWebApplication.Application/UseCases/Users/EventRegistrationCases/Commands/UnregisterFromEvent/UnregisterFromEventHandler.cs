using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.UnregisterFromEvent
{
    public class UnregisterFromEventHandler(
        IEventRegistrationRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<UnregisterFromEventCommand, EventRegistrationReadDto>
    {
        public async Task<EventRegistrationReadDto> Handle(UnregisterFromEventCommand request, CancellationToken cancellationToken)
        {
            var registration = await _repository.GetByIdAsync(request.RegistrationId, cancellationToken);

            if (registration == null)
            {
                throw new NotFoundException($"Not found with id: {request.RegistrationId}", nameof(EventRegistration));
            }

            if (registration.ParticipantId != request.UserId)
            {
                throw new NoPermissionException("Insufficient permissions to perform the operation", nameof(UnregisterFromEventCommand), request.UserId.ToString());
            }

            _repository.Delete(registration);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventRegistrationReadDto>(registration);
        }
    }
}