using FluentValidation;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.CreateEvent
{
    public class CreateEventValidator
    : AbstractValidator<CreateEventCommand>
    {
        public CreateEventValidator()
        {
            RuleFor(dto => dto.Name)
                .NotNullNotEmpty()
                .EventName();

            RuleFor(dto => dto.Description)
                .Description();

            RuleFor(dto => dto.Date)
                .EventDate()
                .NewEventDate();

            RuleFor(dto => dto.Time)
                .EventTime();

            RuleFor(dto => dto.Location)
                .NotNullNotEmpty()
                .EventLocation();

            RuleFor(dto => dto.ImageUrl)
                .ImageUrl();

            RuleFor(dto => dto.MaxParticipants)
                .MaxParticipants();
        }
    }
}