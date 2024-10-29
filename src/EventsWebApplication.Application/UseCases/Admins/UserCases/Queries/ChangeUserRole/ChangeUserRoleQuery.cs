using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.UserCases.Queries.ChangeUserRole
{
    public class ChangeUserRoleQuery : IRequest<UserReadDto>
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
    }
}