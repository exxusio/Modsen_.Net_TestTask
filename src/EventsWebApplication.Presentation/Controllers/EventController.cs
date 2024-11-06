using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EventsWebApplication.Application.Configs.Policies;
using EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.CreateEvent;
using EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.DeleteEvent;
using EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.UpdateEvent;
using EventsWebApplication.Application.UseCases.Admins.EventCases.Queries.GetAllEvents;
using EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventsByFilter;
using EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventById;

namespace EventsWebApplication.Presentation.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventController(
        IMediator mediator
    ) : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = Policies.AdminOnlyActions)]
        public async Task<IActionResult> GetAllEvents([FromQuery] GetAllEventsQuery query, CancellationToken cancellationToken = default)
        {
            var events = await mediator.Send(query, cancellationToken);

            return Ok(events);
        }

        [HttpGet("{eventId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventById(Guid eventId, [FromQuery] GetEventByIdQuery query, CancellationToken cancellationToken = default)
        {
            query.EventId = eventId;

            var _event = await mediator.Send(query, cancellationToken);

            return Ok(_event);
        }

        [HttpGet("filter/page={pageNumber}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventsByFilter(int pageNumber, [FromQuery] GetEventsByFilterQuery query, CancellationToken cancellationToken = default)
        {
            query.PageNumber = pageNumber;
            query.PageSize = 20;

            var events = await mediator.Send(query, cancellationToken);

            return Ok(events);
        }

        [HttpPost]
        [Authorize(Policy = Policies.CreateEvents)]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command, CancellationToken cancellationToken = default)
        {
            var _event = await mediator.Send(command, cancellationToken);

            return Ok(_event);
        }

        [HttpPut("{eventId}")]
        [Authorize(Policy = Policies.UpdateEvents)]
        public async Task<IActionResult> UpdateEvent(Guid eventId, [FromBody] UpdateEventCommand command, CancellationToken cancellationToken = default)
        {
            command.EventId = eventId;

            var _event = await mediator.Send(command, cancellationToken);

            return Ok(_event);
        }

        [HttpDelete("{eventId}")]
        [Authorize(Policy = Policies.DeleteEvents)]
        public async Task<IActionResult> DeleteEvent(Guid eventId, [FromQuery] DeleteEventCommand command, CancellationToken cancellationToken = default)
        {
            command.EventId = eventId;

            var _event = await mediator.Send(command, cancellationToken);

            return Ok(_event);
        }
    }
}