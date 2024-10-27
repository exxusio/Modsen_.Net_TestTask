namespace EventsWebApplication.Application.DTOs.Events
{
    public class EventReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
    }
}