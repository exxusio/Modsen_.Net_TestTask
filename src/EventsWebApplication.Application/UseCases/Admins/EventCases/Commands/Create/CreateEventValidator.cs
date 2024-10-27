using FluentValidation;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.Create
{
    public class CreateEventValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventValidator()
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