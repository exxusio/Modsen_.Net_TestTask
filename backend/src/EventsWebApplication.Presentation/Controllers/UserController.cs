using MediatR;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EventsWebApplication.Application.Configs.Policies;
using EventsWebApplication.Application.UseCases.Admins.UserCases.Commands.ChangeUserRole;
using EventsWebApplication.Application.UseCases.Admins.UserCases.Queries.GetAllUsers;
using EventsWebApplication.Application.UseCases.Admins.UserCases.Queries.GetUser;
using EventsWebApplication.Application.UseCases.Users.UserCases.Commands.ChangeUserPassword;
using EventsWebApplication.Application.UseCases.Users.UserCases.Queries.GetCurrentUser;
using EventsWebApplication.Application.UseCases.Users.UserCases.Commands.CreateUser;
using EventsWebApplication.Application.UseCases.Users.UserCases.Commands.UpdateUser;
using EventsWebApplication.Application.UseCases.Users.UserCases.Commands.LoginUser;

namespace EventsWebApplication.Presentation.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController(
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
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQuery query, CancellationToken cancellationToken = default)
        {
            var users = await mediator.Send(query, cancellationToken);

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [Authorize(Policy = Policies.AdminOnlyActions)]
        public async Task<IActionResult> GetUser(Guid userId, [FromQuery] GetUserQuery query, CancellationToken cancellationToken = default)
        {
            query.UserId = userId;

            var user = await mediator.Send(query, cancellationToken);

            return Ok(user);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe([FromQuery] GetCurrentUserQuery query, CancellationToken cancellationToken = default)
        {
            query.UserId = UserId;

            var user = await mediator.Send(query, cancellationToken);

            return Ok(user);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken = default)
        {
            var user = await mediator.Send(command, cancellationToken);

            return Ok(user);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command, CancellationToken cancellationToken = default)
        {
            var tokens = await mediator.Send(command, cancellationToken);

            return Ok(tokens);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command, CancellationToken cancellationToken = default)
        {
            command.UserId = UserId;

            var user = await mediator.Send(command, cancellationToken);

            return Ok(user);
        }

        [HttpPut("user/password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command, CancellationToken cancellationToken = default)
        {
            command.UserId = UserId;

            var user = await mediator.Send(command, cancellationToken);

            return Ok(user);
        }

        [HttpPut("user/assign-role")]
        [Authorize(Policy = Policies.AdminOnlyActions)]
        public async Task<IActionResult> ChangeUserRole([FromBody] ChangeUserRoleQuery query, CancellationToken cancellationToken = default)
        {
            var user = await mediator.Send(query, cancellationToken);

            return Ok(user);
        }
    }
}