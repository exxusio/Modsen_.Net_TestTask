using MediatR;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs.Users;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordCommand
    : IRequest<UserReadDto>
    {
        [BindNever]
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}