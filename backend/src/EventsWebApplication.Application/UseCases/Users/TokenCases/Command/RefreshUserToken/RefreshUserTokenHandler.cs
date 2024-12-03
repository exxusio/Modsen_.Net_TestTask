using MediatR;
using AutoMapper;
using EventsWebApplication.Application.UseCases.Bases.Handlers.Tokens;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Abstractions.Auth;
using EventsWebApplication.Domain.Abstractions.Data;
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
            var refreshToken = await _unitOfWork.RefreshTokens.GetByIdAsync(request.Key);
            if (refreshToken == null)
            {
                throw new NotFoundException(
                    $"Not found with id",
                    nameof(RefreshToken),
                    nameof(request.Key),
                    request.Key.ToString()
                );
            }

            var user = refreshToken.User;

            _unitOfWork.RefreshTokens.Delete(refreshToken);
            if (!refreshToken.IsActive)
            {
                throw new ExpireException(
                    "The refresh token is expired and can no longer be used"
                );
            }

            return await GenerateAndSaveTokens(user, cancellationToken);
        }
    }
}