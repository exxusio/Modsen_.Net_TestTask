using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.Create
{
    public class CreateEventHandler(
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : IRequestHandler<CreateEventCommand, EventReadDto>
    {
        public async Task<EventReadDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventRepository = _unitOfWork.GetRepository<Event>();

            var existingEvent = (await eventRepository.GetByPredicateAsync(_event => _event.Name == request.Name, cancellationToken)).FirstOrDefault();

            if (existingEvent != null)
            {
                throw new NonUniqueNameException($"An entity with the specified name already exists", nameof(existingEvent), request.Name);
            }

            var categoryRepository = _unitOfWork.GetRepository<EventCategory>();

            var _event = _mapper.Map<Event>(request);

            var existingCategory = await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
            if (existingCategory == null)
            {
                throw new NotFoundException($"Not found with id: {request.CategoryId}", nameof(existingCategory));
            }

            _event.Category = existingCategory;

            await eventRepository.AddAsync(_event, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventReadDto>(_event);
        }
    }
}