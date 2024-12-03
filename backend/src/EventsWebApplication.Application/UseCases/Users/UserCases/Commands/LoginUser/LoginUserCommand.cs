using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.LoginUser
{
    public class LoginUserCommand
    : IRequest<TokensResponse>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}