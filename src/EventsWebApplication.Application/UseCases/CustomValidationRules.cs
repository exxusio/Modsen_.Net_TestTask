using FluentValidation;

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
                .Length(3, 100).WithMessage("{PropertyName} should have length between 3 and 50");
        }

        public static IRuleBuilder<T, string> Description<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(500).WithMessage("{PropertyName} should not exceed 500 characters");
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
                .EmailAddress().WithMessage("{PropertyName} is not a valid email address")
                .MaximumLength(100).WithMessage("{PropertyName} should not exceed 100 characters");
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
                .NotNull().WithMessage("{PropertyName} should not be null")
                .NotEmpty().WithMessage("{PropertyName} should not be empty")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).WithMessage("{PropertyName} should be a valid URL")
                .MaximumLength(250).WithMessage("{PropertyName} should not exceed 250 characters");
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
    }
}