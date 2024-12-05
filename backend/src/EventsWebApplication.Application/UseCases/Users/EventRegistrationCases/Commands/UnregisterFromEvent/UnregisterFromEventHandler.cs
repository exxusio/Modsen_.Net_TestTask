using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Domain.Abstractions.Data;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.UnregisterFromEvent
{
    public class UnregisterFromEventHandler(
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : IRequestHandler<UnregisterFromEventCommand, EventRegistrationReadDto>
    {
        public async Task<EventRegistrationReadDto> Handle(UnregisterFromEventCommand request, CancellationToken cancellationToken)
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

            var registration = await _unitOfWork.EventRegistrations.GetByEventIdAndParticipantIdAsync(request.EventId, request.UserId, cancellationToken);
            if (registration == null)
            {
                throw new NotFoundException(
                    $"Registration not found for the specified event",
                    nameof(Event),
                    nameof(request.EventId),
                    request.EventId.ToString()
                );
            }

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

            _unitOfWork.EventRegistrations.Delete(registration);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventRegistrationReadDto>(registration);
        }
    }
}