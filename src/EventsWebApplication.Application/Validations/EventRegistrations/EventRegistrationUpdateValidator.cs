using FluentValidation;
using EventsWebApplication.Application.DTOs.EventRegistrations;

namespace EventsWebApplication.Application.Validations.EventRegistrations
{
    public class EventRegistrationUpdateValidator : AbstractValidator<EventRegistrationUpdateDto>
    {
        public EventRegistrationUpdateValidator()
        {
            RuleFor(dto => dto.RegistrationDate)
                .EventDateAndRegistrationDate(dto => dto.RegistrationDate);
        }
    }
}