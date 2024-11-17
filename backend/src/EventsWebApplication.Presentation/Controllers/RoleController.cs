using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EventsWebApplication.Application.Configs.Policies;
using EventsWebApplication.Application.UseCases.Admins.RoleCases.Queries.GetAllRoles;
using EventsWebApplication.Application.UseCases.Admins.RoleCases.Queries.GetRoleByName;

namespace EventsWebApplication.Presentation.Controllers
{
    [ApiController]
    [Route("roles")]
    [Authorize(Policy = Policies.AdminOnlyActions)]
    public class RoleController(
        IMediator mediator
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRoles([FromQuery] GetAllRolesQuery query, CancellationToken cancellationToken = default)
        {
            var roles = await mediator.Send(query, cancellationToken);

            return Ok(roles);
        }

        [HttpGet("{roleName}")]
        public async Task<IActionResult> GetRoleByName(string roleName, [FromQuery] GetRoleByNameQuery query, CancellationToken cancellationToken = default)
        {
            query.RoleName = roleName;

            var roles = await mediator.Send(query, cancellationToken);

            return Ok(roles);
        }
    }
}