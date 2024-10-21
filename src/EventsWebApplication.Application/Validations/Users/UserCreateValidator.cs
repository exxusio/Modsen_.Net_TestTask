using FluentValidation;
using EventsWebApplication.Application.DTOs.Users;

namespace EventsWebApplication.Application.Validations.Users
{
    public class UserCreateValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateValidator()
        {
            RuleFor(dto => dto.FirstName)
                .RoleOrUserName();

            RuleFor(dto => dto.LastName)
                .RoleOrUserName();

            RuleFor(dto => dto.DateOfBirth)
                .DateOfBirth();
        }
    }
}