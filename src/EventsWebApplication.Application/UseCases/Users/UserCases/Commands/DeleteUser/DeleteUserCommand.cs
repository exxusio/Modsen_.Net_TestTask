using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs.Users;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.DeleteUser
{
    public class DeleteUserCommand
    : IRequest<UserReadDto>
    {
        [BindNever]
        public Guid Id { get; set; }
    }
}