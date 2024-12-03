using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Abstractions.Auth;
using EventsWebApplication.Domain.Abstractions.Data;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Bases.Handlers.Tokens
{
    public abstract class TokenHandler
    {
        protected readonly ITokensGenerator _tokenGenerator;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        protected TokenHandler(ITokensGenerator tokenGenerator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        protected async Task<TokensResponse> GenerateAndSaveTokens(User user, CancellationToken cancellationToken)
        {
            var token = _tokenGenerator.GenerateRefreshToken();

            var refreshToken = _mapper.Map<RefreshToken>(token);
            refreshToken.User = user;

            await _unitOfWork.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await _unitOfWork.RefreshTokens.SaveChangesAsync(cancellationToken);

            return GenerateTokenResponse(refreshToken);
        }

        protected TokensResponse GenerateAccessTokenForUser(RefreshToken refreshToken)
        {
            return GenerateTokenResponse(refreshToken);
        }

        private TokensResponse GenerateTokenResponse(RefreshToken refreshToken)
        {
            var tokenResponse = new TokensResponse
            {
                AccessToken = _tokenGenerator.GenerateAccessToken(refreshToken.User),
                RefreshToken = _mapper.Map<Token>(refreshToken)
            };

            return tokenResponse;
        }
    }
}