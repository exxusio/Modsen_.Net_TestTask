using FluentValidation;

namespace EventsWebApplication.Application.UseCases.Admins.RoleCases.Commands.CreateRole
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidator()
        {
            RuleFor(dto => dto.Name)
                .RoleOrUserName();
        }
    }
}