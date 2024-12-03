using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Users;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Domain.Abstractions.Auth;
using EventsWebApplication.Domain.Abstractions.Data;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Consts;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.CreateUser
{
    public class CreateUserHandler(
        IPasswordHasher _passwordHasher,
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : IRequestHandler<CreateUserCommand, UserReadDto>
    {
        public async Task<UserReadDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var checkLogin = await _unitOfWork.Users.FindByLoginAsync(request.Login, cancellationToken);
            if (checkLogin != null)
            {
                throw new AlreadyExistsException(
                    "The login is already taken",
                    nameof(User),
                    nameof(request.Login),
                    request.Login
                );
            }

            var checkEmail = await _unitOfWork.Users.FindByEmailAsync(request.Email, cancellationToken);
            if (checkEmail != null)
            {
                throw new AlreadyExistsException(
                    "The email is already in use",
                    nameof(User),
                    nameof(request.Email),
                    request.Email
                );
            }

            var role = await _unitOfWork.Roles.GetByNameAsync(BaseRoles.User, cancellationToken);
            if (role == null)
            {
                throw new NotFoundException(
                    $"Not found with name",
                    nameof(Role),
                    nameof(BaseRoles),
                    BaseRoles.User
                );
            }

            var newUser = _mapper.Map<User>(request);
            newUser.Role = role;
            newUser.RoleId = role.Id;
            newUser.HashPassword = _passwordHasher.HashPassword(request.Password);

            await _unitOfWork.Users.AddAsync(newUser, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserReadDto>(newUser);
        }
    }
}