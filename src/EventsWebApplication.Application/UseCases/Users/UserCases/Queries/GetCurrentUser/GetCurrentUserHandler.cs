using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Users;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Queries.GetCurrentUser
{
    public class GetCurrentUserHandler(
        IUserRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetCurrentUserQuery, UserDetailedReadDto>
    {
        public async Task<UserDetailedReadDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(User),
                    nameof(request.UserId),
                    request.UserId.ToString()
                );
            }

            return _mapper.Map<UserDetailedReadDto>(user);
        }
    }
}