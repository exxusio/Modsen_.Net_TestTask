using AutoMapper;
using EventsWebApplication.Application.DTOs.Tokens;
using EventsWebApplication.Application.Abstractions.Auth;
using EventsWebApplication.Application.Abstractions.Data;
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

            var refreshTokenRepository = _unitOfWork.GetRepository<RefreshToken>();

            await refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
            await refreshTokenRepository.SaveChangesAsync(cancellationToken);

            var tokenResponse = new TokensResponse
            {
                AccessToken = _tokenGenerator.GenerateAccessToken(refreshToken.User),
                RefreshToken = token
            };

            return tokenResponse;
        }
    }
}