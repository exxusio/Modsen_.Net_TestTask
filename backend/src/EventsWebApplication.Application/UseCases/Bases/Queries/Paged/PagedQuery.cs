using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventsWebApplication.Application.UseCases.Bases.Queries.Paged
{
    public abstract class PagedQuery
    {
        [BindNever]
        [JsonIgnore]
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}