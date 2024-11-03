using MediatR;
using EventsWebApplication.Application.DTOs.Users;

namespace EventsWebApplication.Application.UseCases.Admins.UserCases.Queries.GetAllUsers
{
    public class GetAllUsersQuery
    : IRequest<IEnumerable<UserReadDto>>
    {
    }
}