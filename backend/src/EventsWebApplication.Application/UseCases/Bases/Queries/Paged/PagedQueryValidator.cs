using FluentValidation;

namespace EventsWebApplication.Application.UseCases.Bases.Queries.Paged
{
    public class PagedQueryValidator
        : AbstractValidator<PagedQuery>
    {
        public PagedQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .Paged();
            RuleFor(x => x.PageSize)
                .Paged();
        }
    }
}