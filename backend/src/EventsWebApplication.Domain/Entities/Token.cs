namespace EventsWebApplication.Domain.Entities
{
    public class Token
    {
        public string Value { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}