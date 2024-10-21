using FluentValidation;
using EventsWebApplication.Application.DTOs.EventCategories;

namespace EventsWebApplication.Application.Validations.EventCategories
{
    public class EventCategoryCreateValidator : AbstractValidator<EventCategoryCreateDto>
    {
        public EventCategoryCreateValidator()
        {
            RuleFor(dto => dto.Name)
                .EventOrEventCategoryName();

            RuleFor(dto => dto.Description)
                .Description();
        }
    }
}