using MediatR;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.LogoutUser
{
    public class LogoutUserHandler(
        IUnitOfWork _unitOfWork
    ) : IRequestHandler<LogoutUserCommand>
    {
        public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            var refreshTokenRepository = _unitOfWork.GetRepository<RefreshToken>();

            var refreshToken = await refreshTokenRepository.GetByIdAsync(request.Id);
            if (refreshToken == null)
            {
                throw new NotFoundException($"Not found with id: {request.Id}", nameof(RefreshToken));
            }

            var userRepository = _unitOfWork.GetRepository<User>();

            var user = await userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new NotFoundException($"Not found with id: {request.UserId}", nameof(User));
            }

            if (refreshToken.UserId != user.Id)
            {
                throw new NoPermissionException("Insufficient permissions to perform the operation", nameof(LogoutUserCommand), request.UserId.ToString());
            }

            refreshTokenRepository.Delete(refreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}