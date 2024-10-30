using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.UserCases.Queries.GetUser
{
    public class GetUserQuery : IRequest<UserDetailedReadDto>
    {
        public Guid Id { get; set; }
    }
}