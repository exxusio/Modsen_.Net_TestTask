using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs.Users;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Queries.GetCurrentUser
{
    public class GetCurrentUserQuery
    : IRequest<UserDetailedReadDto>
    {
        [BindNever]
        public Guid UserId { get; set; }
    }
}