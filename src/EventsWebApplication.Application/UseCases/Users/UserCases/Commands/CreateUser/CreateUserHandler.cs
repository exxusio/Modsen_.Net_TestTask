using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Users;
using EventsWebApplication.Application.Abstractions.Auth;
using EventsWebApplication.Application.Abstractions.Data;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Exceptions;
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
            var userRepository = _unitOfWork.GetRepository<IUserRepository, User>();

            var checkEmail = await userRepository.FindByEmailAsync(request.Email, cancellationToken);
            if (checkEmail != null)
            {
                throw new AlreadyExistsException(
                    "The email is already in use",
                    nameof(User),
                    nameof(request.Email),
                    request.Email
                );
            }

            var checkLogin = await userRepository.FindByLoginAsync(request.Login, cancellationToken);
            if (checkLogin != null)
            {
                throw new AlreadyExistsException(
                    "The login is already taken",
                    nameof(User),
                    nameof(request.Login),
                    request.Login
                );
            }

            var roleRepository = _unitOfWork.GetRepository<IRoleRepository, Role>();

            var role = await roleRepository.GetByNameAsync(BaseRoles.User, cancellationToken);
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

            await userRepository.AddAsync(newUser, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserReadDto>(newUser);
        }
    }
}