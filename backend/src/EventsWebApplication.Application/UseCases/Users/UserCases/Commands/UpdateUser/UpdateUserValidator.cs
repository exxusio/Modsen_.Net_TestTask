using FluentValidation;

namespace EventsWebApplication.Application.UseCases.Users.UserCases.Commands.UpdateUser
{
    public class UpdateUserValidator
    : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {

            RuleFor(dto => dto.FirstName)
                .RoleOrUserName();

            RuleFor(dto => dto.LastName)
                .RoleOrUserName();

            RuleFor(dto => dto.DateOfBirth)
                .DateOfBirth();

            RuleFor(dto => dto.Email)
                .Email();
        }
    }
}