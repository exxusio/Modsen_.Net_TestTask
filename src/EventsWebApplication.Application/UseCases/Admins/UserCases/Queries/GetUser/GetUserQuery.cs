using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs.Users;

namespace EventsWebApplication.Application.UseCases.Admins.UserCases.Queries.GetUser
{
    public class GetUserQuery
    : IRequest<UserDetailedReadDto>
    {
        [BindNever]
        public Guid UserId { get; set; }
    }
}