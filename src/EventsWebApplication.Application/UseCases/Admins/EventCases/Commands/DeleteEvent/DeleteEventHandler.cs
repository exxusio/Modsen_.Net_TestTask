using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.DeleteEvent
{
    public class DeleteEventHandler(
        IEventRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<DeleteEventCommand, EventReadDto>
    {
        public async Task<EventReadDto> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var _event = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (_event == null)
            {
                throw new NotFoundException($"Not found with id: {request.Id}", nameof(_event));
            }

            _repository.Delete(_event);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventReadDto>(_event);
        }
    }
}