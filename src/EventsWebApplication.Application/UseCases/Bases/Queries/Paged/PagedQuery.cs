namespace EventsWebApplication.Application.UseCases.Bases.Queries.Paged
{
    public abstract class PagedQuery
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}