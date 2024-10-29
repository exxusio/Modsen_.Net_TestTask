using MediatR;
using EventsWebApplication.Application.DTOs.Tokens;

namespace EventsWebApplication.Application.UseCases.Users.TokenCases.Command.RefreshUserToken
{
    public class RefreshUserTokenCommand : IRequest<TokensResponse>
    {
        public Guid Id { get; set; }
    }
}