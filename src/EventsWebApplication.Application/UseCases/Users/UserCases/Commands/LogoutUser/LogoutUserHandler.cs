using MediatR;
using EventsWebApplication.Application.Abstractions.Data;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.LogoutUser
{
    public class LogoutUserHandler(
        IUnitOfWork _unitOfWork
    ) : IRequestHandler<LogoutUserCommand>
    {
        public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            var refreshTokenRepository = _unitOfWork.GetRepository<RefreshToken>();

            var refreshToken = await refreshTokenRepository.GetByIdAsync(request.Key);
            if (refreshToken == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(RefreshToken),
                    nameof(request.Key),
                    request.Key.ToString()
                );
            }

            var userRepository = _unitOfWork.GetRepository<User>();

            var user = await userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(User),
                    nameof(request.Id),
                    request.Id.ToString()
                );
            }

            if (refreshToken.UserId != user.Id)
            {
                throw new NoPermissionException(
                    "Insufficient permissions to perform the operation",
                    request.Id.ToString(),
                    nameof(LogoutUserCommand)
                );
            }

            refreshTokenRepository.Delete(refreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}