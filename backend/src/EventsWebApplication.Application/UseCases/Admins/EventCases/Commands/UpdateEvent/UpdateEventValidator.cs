using FluentValidation;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.UpdateEvent
{
    public class UpdateEventValidator
    : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventValidator()
        {
            RuleFor(dto => dto.Name)
                .NotNullNotEmpty()
                .EventName();

            RuleFor(dto => dto.Description)
                .Description();

            RuleFor(dto => dto.Date)
                .EventDate();

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