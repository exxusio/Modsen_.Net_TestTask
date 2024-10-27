using FluentValidation;

namespace EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.Create
{
    public class CreateEventCategoryValidator : AbstractValidator<CreateEventCategoryCommand>
    {
        public CreateEventCategoryValidator()
        {
            RuleFor(dto => dto.Name)
                .EventOrEventCategoryName();
        }
    }
}