using FluentValidation;
using EventsWebApplication.Application.DTOs.EventCategories;

namespace EventsWebApplication.Application.Validations.EventCategories
{
    public class EventCategoryUpdateValidator : AbstractValidator<EventCategoryUpdateDto>
    {
        public EventCategoryUpdateValidator()
        {
            RuleFor(ec => ec.Name)
                .EventOrEventCategoryName();

            RuleFor(ec => ec.Description)
                .Description();
        }
    }
}