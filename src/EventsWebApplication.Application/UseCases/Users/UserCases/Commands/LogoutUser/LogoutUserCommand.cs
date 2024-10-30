using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.LogoutUser
{
    public class LogoutUserCommand : IRequest
    {
        public Guid Id { get; set; }

        [BindNever]
        public Guid UserId { get; set; }
    }
}