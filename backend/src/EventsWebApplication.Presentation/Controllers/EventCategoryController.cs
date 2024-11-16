using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EventsWebApplication.Application.Configs.Policies;
using EventsWebApplication.Application.UseCases.Users.EventCategoryCases.Queries.GetAllCategories;
using EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.CreateCategory;

namespace EventsWebApplication.Presentation.Controllers
{
    [ApiController]
    [Route("categories")]
    public class EventCategoryController(
        IMediator mediator
    ) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories([FromQuery] GetAllCategoriesQuery query, CancellationToken cancellationToken = default)
        {
            var categories = await mediator.Send(query, cancellationToken);

            return Ok(categories);
        }

        [HttpPost]
        [Authorize(Policy = Policies.CreateEvents)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken = default)
        {
            var category = await mediator.Send(command, cancellationToken);

            return Ok(category);
        }
    }
}