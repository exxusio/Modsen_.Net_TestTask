using MediatR;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs.Users;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.UpdateUser
{
    public class UpdateUserCommand
    : IRequest<UserReadDto>
    {
        [BindNever]
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
    }
}