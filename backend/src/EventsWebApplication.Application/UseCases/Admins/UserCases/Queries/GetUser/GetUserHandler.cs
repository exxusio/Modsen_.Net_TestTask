using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Users;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.UserCases.Queries.GetUser
{
    public class GetUserHandler(
        IUserRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetUserQuery, UserDetailedReadDto>
    {
        public async Task<UserDetailedReadDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
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