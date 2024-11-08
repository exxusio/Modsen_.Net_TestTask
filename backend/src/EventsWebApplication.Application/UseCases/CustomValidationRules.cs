using FluentValidation;
using System.Linq.Expressions;

namespace EventsWebApplication.Application.UseCases
{
    public static class CustomValidationRules
    {
        public static IRuleBuilder<T, string> RoleOrUserName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("{PropertyName} should not be null")
                .NotEmpty().WithMessage("{PropertyName} should not be empty")
                .Length(3, 50).WithMessage("{PropertyName} should have length between 3 and 50");
        }

        public static IRuleBuilder<T, string> EventOrEventCategoryName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("{PropertyName} should not be null")
                .NotEmpty().WithMessage("{PropertyName} should not be empty")
                .Length(3, 100).WithMessage("{PropertyName} should have length between 3 and 100");
        }

        public static IRuleBuilder<T, string> Description<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(500).WithMessage("{PropertyName} should not exceed 500 characters");
        }

        public static IRuleBuilder<T, string> EventLocation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("{PropertyName} should not be null")
                .NotEmpty().WithMessage("{PropertyName} should not be empty")
                .Length(3, 200).WithMessage("{PropertyName} should have length between 3 and 200");
        }

        public static IRuleBuilder<T, string> ImageUrl<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(uri =>
                    string.IsNullOrEmpty(uri) ||
                    Uri.IsWellFormedUriString(uri, UriKind.Absolute)
                    ).WithMessage("Image should be a valid URL");
        }

        public static IRuleBuilder<T, TimeSpan> EventTime<T>(this IRuleBuilder<T, TimeSpan> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("{PropertyName} should not be null");
        }

        public static IRuleBuilder<T, DateTime> EventDate<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("{PropertyName} should not be null")
                .GreaterThan(DateTime.MinValue).WithMessage("{PropertyName} should be a valid date");
        }

        public static IRuleBuilder<T, int> MaxParticipants<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0");
        }

        public static IRuleBuilder<T, int> Paged<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0");
        }

        public static IRuleBuilder<T, string> Login<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("{PropertyName} should not be null")
                .NotEmpty().WithMessage("{PropertyName} should not be empty")
                .Length(6, 20).WithMessage("{PropertyName} should have length between 6 and 20");
        }

        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("{PropertyName} should not be null")
                .NotEmpty().WithMessage("Please, enter the {PropertyName}")
                .Length(8, 20).WithMessage("{PropertyName} must be between 8 and 20 characters");
        }

        public static IRuleBuilder<T, string> ConfirmPassword<T>(this IRuleBuilder<T, string> ruleBuilder, Expression<Func<T, string>> passwordProperty)
        {
            return ruleBuilder
                .Equal(passwordProperty).WithMessage("Password and confirmation do not match");
        }

        public static IRuleBuilder<T, DateTime> DateOfBirth<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("{PropertyName} should not be null")
                .LessThan(DateTime.Now).WithMessage("{PropertyName} cannot be in the future")
                .GreaterThan(DateTime.MinValue).WithMessage("{PropertyName} should be a valid date");
        }

        public static IRuleBuilder<T, string> Email<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("{PropertyName} should not be null")
                .EmailAddress().WithMessage("{PropertyName} is not a valid email address")
                .MaximumLength(150).WithMessage("{PropertyName} should not exceed 150 characters");
        }
    }
}