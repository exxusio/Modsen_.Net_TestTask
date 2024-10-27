using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;

namespace EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.Delete
{
    public class DeleteEventCategoryHandler(
        IEventCategoryRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<DeleteEventCategoryCommand, EventCategoryReadDto>
    {
        public async Task<EventCategoryReadDto> Handle(DeleteEventCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (category == null)
            {
                throw new NotFoundException($"Not found with id: {request.Id}", nameof(category));
            }

            _repository.Delete(category);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventCategoryReadDto>(category);
        }
    }
}