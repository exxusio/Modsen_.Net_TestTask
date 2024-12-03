using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Users.TokenCases.Command.RefreshUserToken
{
    public class RefreshUserTokenCommand
    : IRequest<TokensResponse>
    {
        public Guid Key { get; set; }
    }
}