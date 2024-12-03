using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Abstractions.Caching;
using EventsWebApplication.Domain.Abstractions.Notify;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Consts;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.DeleteEvent
{
    public class DeleteEventHandler(
        ICacheService _cache,
        IEventRepository _repository,
        IMapper _mapper,
        INotificationService _notifyService
    ) : IRequestHandler<DeleteEventCommand, EventReadDto>
    {
        public async Task<EventReadDto> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var _event = await _repository.GetByIdAsync(request.EventId, cancellationToken);
            if (_event == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(Event),
                    nameof(request.EventId),
                    request.EventId.ToString()
                );
            }

            var eventReadDto = _mapper.Map<EventReadDto>(_event);

            await _notifyService.SendToAllEventChange(
                eventReadDto.Id,
                eventReadDto.Name,
                "The event has been deleted",
                NotifyType.EventDeleted,
                cancellationToken
            );

            _repository.Delete(_event);
            await _repository.SaveChangesAsync(cancellationToken);

            await _cache.DeleteAsync<EventReadDto>(eventReadDto.Id.ToString());

            return eventReadDto;
        }
    }
}