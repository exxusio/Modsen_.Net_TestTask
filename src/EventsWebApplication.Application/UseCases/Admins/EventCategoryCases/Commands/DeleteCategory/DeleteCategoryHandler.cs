using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.DeleteCategory
{
    public class DeleteCategoryHandler(
        IEventCategoryRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<DeleteCategoryCommand, EventCategoryReadDto>
    {
        public async Task<EventCategoryReadDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (category == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(Event),
                    nameof(request.Id),
                    request.Id.ToString()
                );
            }

            _repository.Delete(category);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventCategoryReadDto>(category);
        }
    }
}