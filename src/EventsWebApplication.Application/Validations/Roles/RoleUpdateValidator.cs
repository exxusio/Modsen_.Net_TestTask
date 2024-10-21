using FluentValidation;
using EventsWebApplication.Application.DTOs.Roles;

namespace EventsWebApplication.Application.Validations.Roles
{
    public class RoleUpdateValidator : AbstractValidator<RoleUpdateDto>
    {
        public RoleUpdateValidator()
        {
            RuleFor(dto => dto.Name)
                .RoleOrUserName();
        }
    }
}