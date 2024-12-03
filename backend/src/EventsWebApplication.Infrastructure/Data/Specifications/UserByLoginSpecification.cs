using System.Linq.Expressions;
using EventsWebApplication.Infrastructure.Data.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Specifications
{
    public class UserByLoginSpecification(
        string login
    ) : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Login.ToLower() == login.ToLower();
        }
    }
}