using System.Linq.Expressions;
using EventsWebApplication.Domain.Specifications;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Specifications
{
    public class UserByLoginSpecification(
        string login)
        : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Login == login;
        }
    }
}