using MediatR;
using AutoMapper;
using EventsWebApplication.Application.Algorithms.Interfaces;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
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
            var user = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException($"Not found with id: {request.Id}", nameof(User));
            }

            user.HashPassword = _passwordHasher.HashPassword(request.Password);

            await _repository.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserReadDto>(user);
        }
    }
}