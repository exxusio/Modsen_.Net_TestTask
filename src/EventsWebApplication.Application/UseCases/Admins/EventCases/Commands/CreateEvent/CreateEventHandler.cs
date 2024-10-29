using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.CreateEvent
{
    public class CreateEventHandler(
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : IRequestHandler<CreateEventCommand, EventReadDto>
    {
        public async Task<EventReadDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventRepository = _unitOfWork.GetRepository<IEventRepository, Event>();

            var existingEvent = await eventRepository.GetEventByName(request.Name, cancellationToken);

            if (existingEvent != null)
            {
                throw new AlreadyExistsException("An entity with the specified attributes already exists", nameof(Event), nameof(request.Name), request.Name);
            }

            var categoryRepository = _unitOfWork.GetRepository<EventCategory>();

            var _event = _mapper.Map<Event>(request);

            var existingCategory = await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
            if (existingCategory == null)
            {
                throw new NotFoundException($"Not found with id: {request.CategoryId}", nameof(Event));
            }

            _event.Category = existingCategory;

            await eventRepository.AddAsync(_event, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventReadDto>(_event);
        }
    }
}