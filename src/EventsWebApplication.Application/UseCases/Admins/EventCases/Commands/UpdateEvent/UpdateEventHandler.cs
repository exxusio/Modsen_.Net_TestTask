using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Configs.Cache;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.UpdateEvent
{
    public class UpdateEventHandler(
        ICacheRepository _cache,
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : IRequestHandler<UpdateEventCommand, EventReadDto>
    {
        public async Task<EventReadDto> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventRepository = _unitOfWork.GetRepository<IEventRepository, Event>();

            var _event = await eventRepository.GetByIdAsync(request.Id, cancellationToken);
            if (_event == null)
            {
                throw new NotFoundException($"Not found with id: {request.Id}", nameof(Event));
            }

            var existingEvent = await eventRepository.GetEventByNameAsync(request.Name, cancellationToken);

            if (existingEvent != null && existingEvent.Id != _event.Id)
            {
                throw new AlreadyExistsException("An entity with the specified attributes already exists", nameof(Event), nameof(request.Name), request.Name);
            }

            var categoryRepository = _unitOfWork.GetRepository<EventCategory>();

            var existingCategory = await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
            if (existingCategory == null)
            {
                throw new NotFoundException($"Not found with id: {request.CategoryId}", nameof(EventCategory));
            }

            var newEvent = _mapper.Map(request, _event);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var eventReadDto = _mapper.Map<EventReadDto>(newEvent);
            await _cache.SetAsync(eventReadDto.Id.ToString(), eventReadDto, CacheConfig.EVENT_TIME);

            return eventReadDto;
        }
    }
}