using FluentValidation;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordValidator
    : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordValidator()
        {
            RuleFor(dto => dto.Password)
                .Password();

            RuleFor(dto => dto.ConfirmPassword)
                .ConfirmPassword(dto => dto.Password);
        }
    }
}