using FluentValidation;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.UpdateUser
{
    public class UpdateUserValidator
    : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {

            RuleFor(dto => dto.FirstName)
                .NotNullNotEmpty()
                .UserName();

            RuleFor(dto => dto.LastName)
                .NotNullNotEmpty()
                .UserName();

            RuleFor(dto => dto.DateOfBirth)
                .DateOfBirth();

            RuleFor(dto => dto.Email)
                .NotNullNotEmpty()
                .Email();
        }
    }
}