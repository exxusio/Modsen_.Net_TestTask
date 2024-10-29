using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;

namespace EventsWebApplication.Application.UseCases.Admins.UserCases.Queries.GetAllUsers
{
    public class GetAllUsersHandler(
        IUserRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserReadDto>>
    {
        public async Task<IEnumerable<UserReadDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }
    }
}