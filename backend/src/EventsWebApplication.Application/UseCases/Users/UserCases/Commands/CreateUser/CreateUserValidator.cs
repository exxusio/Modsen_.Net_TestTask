using FluentValidation;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.CreateUser
{
    public class CreateUserValidator
    : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(dto => dto.Login)
                .NotNullNotEmpty()
                .Login();

            RuleFor(dto => dto.Password)
                .NotNullNotEmpty()
                .Password();

            RuleFor(dto => dto.ConfirmPassword)
                .ConfirmPassword(dto => dto.Password);

            RuleFor(dto => dto.FirstName)
                .NotNullNotEmpty()
                .UserName();

            RuleFor(dto => dto.LastName)
                .NotNullNotEmpty()
                .UserName();

            RuleFor(dto => dto.Email)
                .NotNullNotEmpty()
                .Email();
        }
    }
}