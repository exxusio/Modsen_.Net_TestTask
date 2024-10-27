using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetByName
{
    public class GetByNameEventHandler(
        IEventRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetByNameEventQuery, EventDetailedReadDto>
    {
        public async Task<EventDetailedReadDto> Handle(GetByNameEventQuery request, CancellationToken cancellationToken)
        {
            var _event = (await _repository.GetByPredicateAsync(eventDto => eventDto.Name == request.Name, cancellationToken)).FirstOrDefault();
            if (_event == null)
            {
                throw new NotFoundException($"Not found with name: {request.Name}", nameof(_event));
            }

            return _mapper.Map<EventDetailedReadDto>(_event);
        }
    }
}