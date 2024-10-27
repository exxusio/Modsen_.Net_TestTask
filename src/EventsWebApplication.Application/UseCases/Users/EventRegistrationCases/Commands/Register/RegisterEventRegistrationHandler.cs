using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Helpers;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.Register
{
    public class RegisterEventRegistrationHandler(
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : IRequestHandler<RegisterEventRegistrationCommand, EventRegistrationReadDto>
    {
        public async Task<EventRegistrationReadDto> Handle(RegisterEventRegistrationCommand request, CancellationToken cancellationToken)
        {
            var eventRepository = _unitOfWork.GetRepository<Event>();

            var _event = await eventRepository.GetByIdAsync(request.EventId, cancellationToken);
            if (_event == null)
            {
                throw new NotFoundException($"Not found with id: {request.EventId}", nameof(_event));
            }

            var userRepository = _unitOfWork.GetRepository<User>();

            var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException($"Not found with id: {request.UserId}", nameof(user));
            }

            var eventRegistrationRepository = _unitOfWork.GetRepository<EventRegistration>();

            var predicate = PredicateBuilder.True<EventRegistration>();

            predicate = predicate.And(registration => registration.EventId == request.EventId);
            predicate = predicate.And(registration => registration.ParticipantId == request.UserId);

            var existingRegistration = (await eventRegistrationRepository.GetByPredicateAsync(predicate, cancellationToken)).FirstOrDefault();

            if (existingRegistration != null)
            {
                throw new DuplicateRegistrationException(
                    "User is already registered for this event",
                    request.UserId.ToString(),
                    request.EventId.ToString()
                );
            }

            var registration = _mapper.Map<EventRegistration>(request);

            await eventRegistrationRepository.AddAsync(registration);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<EventRegistrationReadDto>(registration);
        }
    }
}