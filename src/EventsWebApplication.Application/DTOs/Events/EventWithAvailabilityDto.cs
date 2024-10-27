namespace EventsWebApplication.Application.DTOs.Events
{
    public class EventWithAvailabilityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public bool HasAvailableSeats { get; set; }
    }
}