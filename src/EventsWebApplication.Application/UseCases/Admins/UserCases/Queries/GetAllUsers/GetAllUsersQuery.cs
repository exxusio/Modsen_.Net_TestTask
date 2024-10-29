using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.UserCases.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserReadDto>>
    {
    }
}