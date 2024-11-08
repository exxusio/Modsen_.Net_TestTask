using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs.Tokens;
using EventsWebApplication.Application.Abstractions.Auth;
using EventsWebApplication.Application.Abstractions.Data;
using EventsWebApplication.Application.UseCases.Bases.Handlers.Tokens;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.LoginUser
{
    public class LoginUserHandler(
        IPasswordHasher _passwordHasher,
        ITokensGenerator _tokenGenerator,
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : TokenHandler(_tokenGenerator, _unitOfWork, _mapper), IRequestHandler<LoginUserCommand, TokensResponse>
    {
        public async Task<TokensResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.GetRepository<IUserRepository, User>();

            var user = await userRepository.FindByLoginAsync(request.Login, cancellationToken);
            if (user == null)
            {
                throw new UnauthorizedException(
                    "User not found"
                );
            }

            var succeeded = _passwordHasher.VerifyPassword(user.HashPassword, request.Password);
            if (!succeeded)
            {
                throw new UnauthorizedException(
                    "Incorrect login or password"
                );
            }

            return await GenerateAndSaveTokens(user, cancellationToken);
        }
    }
}