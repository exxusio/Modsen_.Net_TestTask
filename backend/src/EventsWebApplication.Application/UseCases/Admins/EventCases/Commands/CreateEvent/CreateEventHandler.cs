using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Domain.Abstractions.Data;
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
            var existingEvent = await _unitOfWork.Events.GetByNameAsync(request.Name, cancellationToken);
            if (existingEvent != null)
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

            var _event = _mapper.Map<Event>(request);
            _event.Category = existingCategory;

            await _unitOfWork.Events.AddAsync(_event, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventReadDto>(_event);
        }
    }
}