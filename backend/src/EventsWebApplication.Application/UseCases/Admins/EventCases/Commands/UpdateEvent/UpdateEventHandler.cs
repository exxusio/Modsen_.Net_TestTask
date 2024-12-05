using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Domain.Abstractions.Notify;
using EventsWebApplication.Domain.Abstractions.Data;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Consts;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.UpdateEvent
{
    public class UpdateEventHandler(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        INotificationService _notifyService
    ) : IRequestHandler<UpdateEventCommand, EventReadDto>
    {
        public async Task<EventReadDto> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
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

            var existingEvent = await _unitOfWork.Events.GetByNameAsync(request.Name, cancellationToken);
            if (existingEvent != null && existingEvent.Id != _event.Id)
            {
                throw new AlreadyExistsException(
                    "An entity with the specified attributes already exists",
                    nameof(Event),
                    nameof(request.Name),
                    request.Name
                );
            }

            var existingCategory = await _unitOfWork.EventCategories.GetByIdAsync(request.CategoryId, cancellationToken);
            if (existingCategory == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(EventCategory),
                    nameof(request.CategoryId),
                    request.CategoryId.ToString()
                );
            }

            var newEvent = _mapper.Map(request, _event);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _notifyService.SendToAllEventChange(
                newEvent.Id,
                _event.Name,
                "The event has been updated",
                NotifyType.EventUpdated,
                cancellationToken
            );

            return _mapper.Map<EventReadDto>(newEvent);
        }
    }
}