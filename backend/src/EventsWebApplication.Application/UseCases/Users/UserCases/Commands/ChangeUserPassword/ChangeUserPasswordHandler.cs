using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Users;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Abstractions.Auth;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordHandler(
        IPasswordHasher _passwordHasher,
        IUserRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<ChangeUserPasswordCommand, UserReadDto>
    {
        public async Task<UserReadDto> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
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

            user.HashPassword = _passwordHasher.HashPassword(request.Password);

            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserReadDto>(user);
        }
    }
}