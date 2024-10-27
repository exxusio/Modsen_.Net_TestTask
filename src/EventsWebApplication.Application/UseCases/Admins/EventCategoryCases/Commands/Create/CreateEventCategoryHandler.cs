using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.Create
{
    public class CreateEventCategoryHandler(
        IEventCategoryRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<CreateEventCategoryCommand, EventCategoryReadDto>
    {
        public async Task<EventCategoryReadDto> Handle(CreateEventCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = (await _repository.GetByPredicateAsync(category => category.Name == request.Name, cancellationToken)).FirstOrDefault();

            if (existingCategory != null)
            {
                throw new NonUniqueNameException($"An entity with the specified name already exists", nameof(existingCategory), request.Name);
            }

            var category = _mapper.Map<EventCategory>(request);

            await _repository.AddAsync(category, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventCategoryReadDto>(category);
        }
    }
}