using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.RoleCases.Commands.Create
{
    public class CreateRoleHandler(
        IRoleRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<CreateRoleCommand, RoleReadDto>
    {
        public async Task<RoleReadDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var existingRole = (await _repository.GetByPredicateAsync(role => role.Name == request.Name, cancellationToken)).FirstOrDefault();

            if (existingRole != null)
            {
                throw new NonUniqueNameException($"An entity with the specified name already exists", nameof(existingRole), request.Name);
            }

            var role = _mapper.Map<Role>(request);

            await _repository.AddAsync(role, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<RoleReadDto>(role);
        }
    }
}