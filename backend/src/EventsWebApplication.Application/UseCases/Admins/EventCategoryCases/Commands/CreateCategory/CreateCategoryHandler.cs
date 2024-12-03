using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.CreateCategory
{
    public class CreateCategoryHandler(
        IEventCategoryRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<CreateCategoryCommand, EventCategoryReadDto>
    {
        public async Task<EventCategoryReadDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _repository.GetByNameAsync(request.Name, cancellationToken);
            if (existingCategory != null)
            {
                throw new AlreadyExistsException(
                    "An entity with the specified attributes already exists",
                    nameof(EventCategory), nameof(request.Name),
                    request.Name
                );
            }

            var category = _mapper.Map<EventCategory>(request);

            await _repository.AddAsync(category, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventCategoryReadDto>(category);
        }
    }
}