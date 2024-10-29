using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.RegisterForEvent
{
    public class RegisterForEventHandler(
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : IRequestHandler<RegisterForEventCommand, EventRegistrationReadDto>
    {
        public async Task<EventRegistrationReadDto> Handle(RegisterForEventCommand request, CancellationToken cancellationToken)
        {
            var eventRepository = _unitOfWork.GetRepository<Event>();

            var _event = await eventRepository.GetByIdAsync(request.EventId, cancellationToken);
            if (_event == null)
            {
                throw new NotFoundException($"Not found with id: {request.EventId}", nameof(Event));
            }

            var userRepository = _unitOfWork.GetRepository<User>();

            var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException($"Not found with id: {request.UserId}", nameof(User));
            }

            var eventRegistrationRepository = _unitOfWork.GetRepository<IEventRegistrationRepository, EventRegistration>();

            var existingRegistration = await eventRegistrationRepository.GetRegistrationByEventIdAndParticipantId(request.UserId, request.EventId, cancellationToken);

            if (existingRegistration != null)
            {
                throw new DuplicateRegistrationException(
                    "User is already registered for this event",
                    request.UserId.ToString(),
                    request.EventId.ToString()
                );
            }

            var registration = _mapper.Map<EventRegistration>(request);

            await eventRegistrationRepository.AddAsync(registration);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<EventRegistrationReadDto>(registration);
        }
    }
}