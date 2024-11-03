using System.Linq.Expressions;
using EventsWebApplication.Domain.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Specifications
{
    public class UserByEmailSpecification(
        string email
    ) : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Email == email;
        }
    }
}