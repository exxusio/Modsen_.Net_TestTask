using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Users;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.UpdateUser
{
    public class UpdateUserHandler(
        IUserRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<UpdateUserCommand, UserReadDto>
    {
        public async Task<UserReadDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(User),
                    nameof(request.Id),
                    request.Id.ToString()
                );
            }

            var checkEmail = await _repository.FindByEmailAsync(request.Email, cancellationToken);
            if (checkEmail != null && checkEmail.Id != user.Id)
            {
                throw new AlreadyExistsException(
                    "The email is already in use",
                    nameof(User),
                    nameof(request.Email),
                    request.Email
                );
            }

            var newUser = _mapper.Map(request, user);

            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserReadDto>(newUser);
        }
    }
}