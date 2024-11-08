using MediatR;
using EventsWebApplication.Application.DTOs.Users;

namespace EventsWebApplication.Application.UseCases.Admins.UserCases.Commands.ChangeUserRole
{
    public class ChangeUserRoleQuery
    : IRequest<UserReadDto>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}