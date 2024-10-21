using FluentValidation;
using EventsWebApplication.Application.DTOs.Events;

namespace EventsWebApplication.Application.Validations.Events
{
    public class EventUpdateValidator : AbstractValidator<EventUpdateDto>
    {
        public EventUpdateValidator()
        {
            RuleFor(dto => dto.Name)
                .EventOrEventCategoryName();

            RuleFor(dto => dto.Description)
                .Description();

            RuleFor(dto => dto.Date)
                .EventDateAndRegistrationDate(dto => dto.Date);

            RuleFor(dto => dto.Time)
                .EventTime();

            RuleFor(dto => dto.Location)
                .EventLocation();

            RuleFor(dto => dto.ImageUrl)
                .ImageUrl();

            RuleFor(dto => dto.MaxParticipants)
                .MaxParticipants();
        }
    }
}