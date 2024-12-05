using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Domain.Abstractions.Data;
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
            var _event = await _unitOfWork.Events.GetByIdAsync(request.EventId, cancellationToken);
            if (_event == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(Event),
                    nameof(request.EventId),
                    request.EventId.ToString()
                );
            }

            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(User),
                    nameof(request.UserId),
                    request.UserId.ToString()
                );
            }

            var existingRegistration = await _unitOfWork.EventRegistrations.GetByEventIdAndParticipantIdAsync(request.EventId, request.UserId, cancellationToken);
            if (existingRegistration != null)
            {
                throw new BadRequestException(
                    "User is already registered for this event",
                    new
                    {
                        UserId = request.UserId.ToString(),
                        EventId = request.EventId.ToString()
                    }
                );
            }

            if (_event.EventRegistrations.Count() >= _event.MaxParticipants)
            {
                throw new BadRequestException(
                    "No available seats for this event",
                    new
                    {
                        UserId = request.UserId.ToString(),
                        EventId = request.EventId.ToString(),
                        MaxRegistrations = _event.MaxParticipants.ToString(),
                        CurrentRegistrations = _event.EventRegistrations.Count().ToString()
                    }
                );
            }

            var registration = _mapper.Map<EventRegistration>(request);

            if (_event.Date < registration.RegistrationDate)
            {
                throw new BadRequestException(
                    "The event has already passed",
                    new
                    {
                        UserId = request.UserId.ToString(),
                        EventId = request.EventId.ToString(),
                        RegistrationDate = registration.RegistrationDate.ToString(),
                        EventDate = _event.Date.ToString()
                    }
                );
            }

            await _unitOfWork.EventRegistrations.AddAsync(registration, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventRegistrationReadDto>(registration);
        }
    }
}