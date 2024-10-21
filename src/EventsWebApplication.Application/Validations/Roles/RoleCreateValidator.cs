using FluentValidation;
using EventsWebApplication.Application.DTOs.Roles;

namespace EventsWebApplication.Application.Validations.Roles
{
    public class RoleCreateValidator : AbstractValidator<RoleCreateDto>
    {
        public RoleCreateValidator()
        {
            RuleFor(dto => dto.Name)
                .RoleOrUserName();
        }
    }
}