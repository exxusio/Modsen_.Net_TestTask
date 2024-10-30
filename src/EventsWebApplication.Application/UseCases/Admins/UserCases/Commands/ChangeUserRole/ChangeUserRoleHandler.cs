using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Application.UseCases.Admins.UserCases.Commands.ChangeUserRole
{
    public class ChangeUserRoleHandler(
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : IRequestHandler<ChangeUserRoleQuery, UserReadDto>
    {
        public async Task<UserReadDto> Handle(ChangeUserRoleQuery request, CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.GetRepository<User>();

            var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException($"Not found with id: {request.Id}", nameof(User));
            }

            var roleRepository = _unitOfWork.GetRepository<IRoleRepository, Role>();

            var role = await roleRepository.GetByIdAsync(request.RoleId, cancellationToken);
            if (role == null)
            {
                throw new NotFoundException($"Not found with id: {request.RoleId}", nameof(Role));
            }

            user.Role = role;
            user.RoleId = role.Id;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserReadDto>(user);
        }
    }
}