using MediatR;
using AutoMapper;
using EventsWebApplication.Application.UseCases.Bases.Handlers.Tokens;
using EventsWebApplication.Application.Algorithms.Interfaces;
using EventsWebApplication.Application.DTOs.Tokens;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.TokenCases.Command.RefreshUserToken
{
    public class RefreshUserTokenHandler(
        ITokensGenerator _tokenGenerator,
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : TokenHandler(_tokenGenerator, _unitOfWork, _mapper), IRequestHandler<RefreshUserTokenCommand, TokensResponse>
    {
        public async Task<TokensResponse> Handle(RefreshUserTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshTokenRepository = _unitOfWork.GetRepository<IRefreshTokenRepository, RefreshToken>();

            var refreshToken = await refreshTokenRepository.GetByIdAsync(request.Id);

            if (refreshToken == null)
            {
                throw new NotFoundException($"Not found with id: {request.Id}", nameof(RefreshToken));
            }

            var user = refreshToken.User;
            refreshTokenRepository.Delete(refreshToken);

            if (!refreshToken.IsActive)
            {
                throw new ExpireException("The refresh token is expired and can no longer be used");
            }

            return await GenerateAndSaveTokens(user, cancellationToken);
        }
    }
}