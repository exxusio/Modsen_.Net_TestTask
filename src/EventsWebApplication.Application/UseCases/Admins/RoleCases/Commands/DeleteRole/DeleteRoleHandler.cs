using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.RoleCases.Commands.DeleteRole
{
    public class DeleteRoleHandler(
        IRoleRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<DeleteRoleCommand, RoleReadDto>
    {
        public async Task<RoleReadDto> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (role == null)
            {
                throw new NotFoundException($"Not found with id: {request.Id}", nameof(Role));
            }

            _repository.Delete(role);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<RoleReadDto>(role);
        }
    }
}