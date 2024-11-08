using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Abstractions.Data;
using EventsWebApplication.Application.Abstractions.Caching;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.UpdateEvent
{
    public class UpdateEventHandler(
        ICacheService _cache,
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : IRequestHandler<UpdateEventCommand, EventReadDto>
    {
        public async Task<EventReadDto> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventRepository = _unitOfWork.GetRepository<IEventRepository, Event>();

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

            var existingEvent = await eventRepository.GetByNameAsync(request.Name, cancellationToken);
            if (existingEvent != null && existingEvent.Id != _event.Id)
            {
                throw new AlreadyExistsException(
                    "An entity with the specified attributes already exists",
                    nameof(Event),
                    nameof(request.Name),
                    request.Name
                );
            }

            var categoryRepository = _unitOfWork.GetRepository<EventCategory>();

            var existingCategory = await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
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

            var eventReadDto = _mapper.Map<EventReadDto>(newEvent);
            await _cache.SetAsync(eventReadDto.Id.ToString(), eventReadDto);

            return eventReadDto;
        }
    }
}