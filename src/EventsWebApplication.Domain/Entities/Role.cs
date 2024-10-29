using EventsWebApplication.Domain.Entities.Interfaces;

namespace EventsWebApplication.Domain.Entities
{
    public class Role : BaseModel, IHaveName
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}