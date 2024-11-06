using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Users;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.DeleteUser
{
    public class DeleteUserHandler(
        IUserRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<DeleteUserCommand, UserReadDto>
    {
        public async Task<UserReadDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
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

            _repository.Delete(user);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserReadDto>(user);
        }
    }
}