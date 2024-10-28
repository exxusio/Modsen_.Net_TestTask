using FluentValidation;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.UpdateEvent
{
    public class UpdateEventValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventValidator()
        {
            RuleFor(dto => dto.Name)
                .EventOrEventCategoryName();

            RuleFor(dto => dto.Description)
                .Description();

            RuleFor(dto => dto.Date)
                .EventDate(dto => dto.Date);

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