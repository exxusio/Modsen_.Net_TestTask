using MediatR;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.LogoutUser
{
    public class LogoutUserCommand
    : IRequest
    {
        public Guid Key { get; set; }

        [BindNever]
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}