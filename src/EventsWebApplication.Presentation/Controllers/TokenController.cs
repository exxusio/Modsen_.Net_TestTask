using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EventsWebApplication.Application.UseCases.Users.TokenCases.Command.RefreshUserToken;

namespace EventsWebApplication.Presentation.Controllers
{
    [ApiController]
    [Route("tokens")]
    public class TokenController(
        IMediator mediator
    ) : ControllerBase
    {
        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshUserToken([FromBody] RefreshUserTokenCommand command, CancellationToken cancellationToken = default)
        {
            var tokenResponse = await mediator.Send(command, cancellationToken);

            return Ok(tokenResponse);
        }
    }
}