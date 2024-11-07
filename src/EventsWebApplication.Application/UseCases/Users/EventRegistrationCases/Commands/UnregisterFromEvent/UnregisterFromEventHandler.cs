using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Abstractions.Data;
using EventsWebApplication.Application.Abstractions.Caching;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.UnregisterFromEvent
{
    public class UnregisterFromEventHandler(
        ICacheService _cache,
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : IRequestHandler<UnregisterFromEventCommand, EventRegistrationReadDto>
    {
        public async Task<EventRegistrationReadDto> Handle(UnregisterFromEventCommand request, CancellationToken cancellationToken)
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

            var registration = await eventRegistrationRepository.GetByEventIdAndParticipantIdAsync(request.EventId, request.UserId, cancellationToken);
            if (registration == null)
            {
                throw new NotFoundException(
                    $"Registration not found for the specified event",
                    nameof(Event),
                    nameof(request.EventId),
                    request.EventId.ToString()
                );
            }

            eventRegistrationRepository.Delete(registration);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var registrationReadDto = _mapper.Map<EventRegistrationReadDto>(registration);
            await _cache.SetAsync(registrationReadDto.Event.Id.ToString(), registrationReadDto.Event);

            return registrationReadDto;
        }
    }
}