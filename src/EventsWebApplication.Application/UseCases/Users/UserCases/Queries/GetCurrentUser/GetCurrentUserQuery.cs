using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Queries.GetCurrentUser
{
    public class GetCurrentUserQuery : IRequest<UserDetailedReadDto>
    {
        [BindNever]
        public Guid Id { get; set; }
    }
}