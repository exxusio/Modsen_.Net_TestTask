using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Abstractions.Caching;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.DeleteEvent
{
    public class DeleteEventHandler(
        ICacheService _cache,
        IEventRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<DeleteEventCommand, EventReadDto>
    {
        public async Task<EventReadDto> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var _event = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (_event == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(Event),
                    nameof(request.Id),
                    request.Id.ToString()
                );
            }

            _repository.Delete(_event);
            await _repository.SaveChangesAsync(cancellationToken);

            var eventReadDto = _mapper.Map<EventReadDto>(_event);
            await _cache.DeleteAsync<EventReadDto>(eventReadDto.Id.ToString());

            return eventReadDto;
        }
    }
}