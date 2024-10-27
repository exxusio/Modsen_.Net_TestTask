using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.RoleCases.Commands.Create
{
    public class CreateRoleCommand : IRequest<RoleReadDto>
    {
        public string Name { get; set; }
    }
}