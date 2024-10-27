using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.RoleCases.Queries.GetAll
{
    public class GetAllRolesQuery : IRequest<IEnumerable<RoleReadDto>>
    {
    }
}