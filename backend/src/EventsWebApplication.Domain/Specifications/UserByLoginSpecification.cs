using System.Linq.Expressions;
using EventsWebApplication.Domain.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Specifications
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