using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.RoleCases.Queries.GetAllRoles
{
    public class GetAllRolesQuery
    : IRequest<IEnumerable<RoleReadDto>>
    {
    }
}