using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Abstractions.Caching;
using EventsWebApplication.Domain.Abstractions.Data;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.RegisterForEvent
{
    public class RegisterForEventHandler(
        ICacheService _cache,
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
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(Event),
                    nameof(request.EventId),
                    request.EventId.ToString()
                );
            }

            var userRepository = _unitOfWork.GetRepository<User>();

            var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(User),
                    nameof(request.UserId),
                    request.UserId.ToString()
                );
            }

            var eventRegistrationRepository = _unitOfWork.GetRepository<IEventRegistrationRepository, EventRegistration>();

            var existingRegistration = await eventRegistrationRepository.GetByEventIdAndParticipantIdAsync(request.EventId, request.UserId, cancellationToken);
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

            await eventRegistrationRepository.AddAsync(registration, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var registrationReadDto = _mapper.Map<EventRegistrationReadDto>(registration);
            await _cache.SetAsync(registrationReadDto.Event.Id.ToString(), registrationReadDto.Event);

            return registrationReadDto;
        }
    }
}