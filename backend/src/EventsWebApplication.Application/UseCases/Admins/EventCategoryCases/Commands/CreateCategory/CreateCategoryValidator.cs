using FluentValidation;

namespace EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.CreateCategory
{
    public class CreateCategoryValidator
    : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(dto => dto.Name)
                .NotNullNotEmpty()
                .EventCategoryName();
        }
    }
}