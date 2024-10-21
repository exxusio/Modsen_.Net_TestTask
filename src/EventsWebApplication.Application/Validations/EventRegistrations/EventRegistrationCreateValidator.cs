using FluentValidation;
using EventsWebApplication.Application.DTOs.EventRegistrations;

namespace EventsWebApplication.Application.Validations.EventRegistrations
{
    public class EventRegistrationCreateValidator : AbstractValidator<EventRegistrationCreateDto>
    {
        public EventRegistrationCreateValidator()
        {
            RuleFor(dto => dto.RegistrationDate)
                .EventDateAndRegistrationDate(dto => dto.RegistrationDate);
        }
    }
}