using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
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
            var user = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException($"Not found with id: {request.Id}", nameof(User));
            }

            return _mapper.Map<UserDetailedReadDto>(user);
        }
    }
}