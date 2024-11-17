using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.RoleCases.Queries.GetRoleByName
{
    public class GetRoleByNameHandler(
        IRoleRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetRoleByNameQuery, RoleReadDto>
    {
        public async Task<RoleReadDto> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetByNameAsync(request.RoleName, cancellationToken);
            if (role == null)
            {
                throw new NotFoundException(
                    $"Not found with name",
                    nameof(Role),
                    nameof(request.RoleName),
                    request.RoleName
                );
            }

            return _mapper.Map<RoleReadDto>(role);
        }
    }
}