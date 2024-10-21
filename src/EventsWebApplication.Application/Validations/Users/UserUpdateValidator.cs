using FluentValidation;
using EventsWebApplication.Application.DTOs.Users;

namespace EventsWebApplication.Application.Validations.Users
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateValidator()
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