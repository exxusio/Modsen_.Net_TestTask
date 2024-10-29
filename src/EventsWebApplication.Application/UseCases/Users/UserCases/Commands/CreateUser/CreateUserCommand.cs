using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserReadDto>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}