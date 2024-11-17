using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.RoleCases.Queries.GetRoleByName
{
    public class GetRoleByNameQuery
    : IRequest<RoleReadDto>
    {
        [BindNever]
        public string RoleName { get; set; } = "";
    }
}