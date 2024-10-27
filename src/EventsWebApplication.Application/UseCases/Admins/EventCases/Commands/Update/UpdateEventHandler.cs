using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.Create
{
    public class UpdateEventHandler(
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : IRequestHandler<UpdateEventCommand, EventReadDto>
    {
        public async Task<EventReadDto> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventRepository = _unitOfWork.GetRepository<Event>();

            var _event = await eventRepository.GetByIdAsync(request.Id, cancellationToken);
            if (_event == null)
            {
                throw new NotFoundException($"Not found with id: {request.Id}", nameof(_event));
            }

            var existingEvent = (await eventRepository.GetByPredicateAsync(_event => _event.Name == request.Name, cancellationToken)).FirstOrDefault();

            if (existingEvent != null && existingEvent.Id != _event.Id)
            {
                throw new NonUniqueNameException($"An entity with the specified name already exists", nameof(existingEvent), request.Name);
            }

            var categoryRepository = _unitOfWork.GetRepository<EventCategory>();

            var existingCategory = await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
            if (existingCategory == null)
            {
                throw new NotFoundException($"Not found with id: {request.CategoryId}", nameof(existingCategory));
            }

            var newEvent = _mapper.Map(request, _event);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<EventReadDto>(newEvent);
        }
    }
}