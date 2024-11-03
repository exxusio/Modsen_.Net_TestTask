using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.LogoutUser
{
    public class LogoutUserCommand
    : IRequest
    {
        public Guid Key { get; set; }

        [BindNever]
        public Guid Id { get; set; }
    }
}