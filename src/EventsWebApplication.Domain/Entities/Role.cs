using EventsWebApplication.Domain.Entities.Interfaces;
using EventsWebApplication.Domain.Entities.Bases;

namespace EventsWebApplication.Domain.Entities
{
    public class Role
    : BaseModel, IHaveName
    {
        public string Name { get; set; }

        public virtual IEnumerable<User> Users { get; set; }
    }
}