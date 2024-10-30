using System.Linq.Expressions;
using EventsWebApplication.Domain.Specifications;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Specifications
{
    public class UserByEmailSpecification(
        string email)
        : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Email == email;
        }
    }
}