using FluentValidation;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(dto => dto.Login)
                .Login();

            RuleFor(dto => dto.Password)
                .Password();

            RuleFor(dto => dto.ConfirmPassword)
                .Password()
                .ConfirmPassword(dto => dto.Password);

            RuleFor(dto => dto.FirstName)
                .RoleOrUserName();

            RuleFor(dto => dto.LastName)
                .RoleOrUserName();

            RuleFor(dto => dto.Email)
                .Email();
        }
    }
}