using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Queries.GetRegistrationDetails
{
    public class GetRegistrationDetailsHandler(
        IEventRegistrationRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetRegistrationDetailsQuery, EventRegistrationReadDto>
    {
        public async Task<EventRegistrationReadDto> Handle(GetRegistrationDetailsQuery request, CancellationToken cancellationToken)
        {
            var registration = await _repository.GetByEventIdAndParticipantIdAsync(request.EventId, request.UserId, cancellationToken);
            if (registration == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(EventRegistration),
                    $"{nameof(request.UserId)}  :::  {nameof(request.EventId)}",
                    $"{request.UserId}  :::  {request.EventId}"
                );
            }

            return _mapper.Map<EventRegistrationReadDto>(registration);
        }
    }
}