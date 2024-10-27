using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.RoleCases.Commands.Delete
{
    public class DeleteRoleCommand : IRequest<RoleReadDto>
    {
        public Guid Id { get; set; }
    }
}