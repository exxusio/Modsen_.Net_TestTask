using MediatR;
using EventsWebApplication.Application.DTOs.Events;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetById
{
    public class GetByIdEventQuery : IRequest<EventDetailedReadDto>
    {
        public Guid Id { get; set; }
    }
}