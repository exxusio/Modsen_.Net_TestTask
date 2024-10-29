using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
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
            var user = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException($"Not found with id: {request.Id}", nameof(User));
            }

            _repository.Delete(user);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserReadDto>(user);
        }
    }
}