using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Users;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Domain.Abstractions.Data;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Admins.UserCases.Commands.ChangeUserRole
{
    public class ChangeUserRoleHandler(
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : IRequestHandler<ChangeUserRoleQuery, UserReadDto>
    {
        public async Task<UserReadDto> Handle(ChangeUserRoleQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(User),
                    nameof(request.UserId),
                    request.UserId.ToString()
                );
            }

            var role = await _unitOfWork.Roles.GetByIdAsync(request.RoleId, cancellationToken);
            if (role == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(EventCategory),
                    nameof(request.RoleId),
                    request.RoleId.ToString()
                );
            }

            user.Role = role;
            user.RoleId = role.Id;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserReadDto>(user);
        }
    }
}