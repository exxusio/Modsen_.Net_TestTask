using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.RoleCases.Commands.CreateRole
{
    public class CreateRoleHandler(
        IRoleRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<CreateRoleCommand, RoleReadDto>
    {
        public async Task<RoleReadDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var existingRole = await _repository.GetRoleByName(request.Name, cancellationToken);

            if (existingRole != null)
            {
                throw new AlreadyExistsException("An entity with the specified attributes already exists", nameof(Role), nameof(request.Name), request.Name);
            }

            var role = _mapper.Map<Role>(request);

            await _repository.AddAsync(role, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<RoleReadDto>(role);
        }
    }
}