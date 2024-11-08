using MediatR;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EventsWebApplication.Application.Configs.Policies;
using EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetRegistrationDetails;
using EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetEventRegistrations;
using EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetAllRegistrations;
using EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Queries.GetUserRegistrations;
using EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.UnregisterFromEvent;
using EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.RegisterForEvent;

namespace EventsWebApplication.Presentation.Controllers
{
    [ApiController]
    [Route("registrations")]
    public class EventRegistrationController(
        IMediator mediator
    ) : ControllerBase
    {
        protected Guid UserId =>
            Guid.TryParse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                out Guid userId
            )
            ? userId
            : Guid.Empty;

        [HttpGet]
        [Authorize(Policy = Policies.AdminOnlyActions)]
        public async Task<IActionResult> GetAllRegistrations([FromQuery] GetAllRegistrationsQuery query, CancellationToken cancellationToken = default)
        {
            var registrations = await mediator.Send(query, cancellationToken);

            return Ok(registrations);
        }

        [HttpGet("{eventId}/{userId}")]
        [Authorize(Policy = Policies.AdminOnlyActions)]
        public async Task<IActionResult> GetRegistrationDetails(Guid eventId, Guid userId, [FromQuery] GetRegistrationDetailsQuery query, CancellationToken cancellationToken = default)
        {
            query.EventId = eventId;
            query.UserId = userId;

            var registration = await mediator.Send(query, cancellationToken);

            return Ok(registration);
        }

        [HttpGet("event/{eventId}")]
        [Authorize(Policy = Policies.AdminOnlyActions)]
        public async Task<IActionResult> GetEventRegistrations(Guid eventId, [FromQuery] GetEventRegistrationsQuery query, CancellationToken cancellationToken = default)
        {
            query.EventId = eventId;

            var registrations = await mediator.Send(query, cancellationToken);

            return Ok(registrations);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetUserRegistrations([FromQuery] GetUserRegistrationsQuery query, CancellationToken cancellationToken = default)
        {
            query.UserId = UserId;

            var registrations = await mediator.Send(query, cancellationToken);

            return Ok(registrations);
        }

        [HttpPost("{eventId}")]
        [Authorize(Policy = Policies.RegisterForEvent)]
        public async Task<IActionResult> RegisterForEvent(Guid eventId, [FromQuery] RegisterForEventCommand command, CancellationToken cancellationToken = default)
        {
            command.EventId = eventId;
            command.UserId = UserId;

            var registration = await mediator.Send(command, cancellationToken);

            return Ok(registration);
        }

        [HttpDelete("{eventId}")]
        [Authorize(Policy = Policies.UnregisterFromEvent)]
        public async Task<IActionResult> UnregisterFromEvent(Guid eventId, [FromQuery] UnregisterFromEventCommand command, CancellationToken cancellationToken = default)
        {
            command.EventId = eventId;
            command.UserId = UserId;

            var registration = await mediator.Send(command, cancellationToken);

            return Ok(registration);
        }
    }
}