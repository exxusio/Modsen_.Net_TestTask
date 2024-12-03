using System.Linq.Expressions;
using EventsWebApplication.Infrastructure.Data.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Specifications
{
    public class UserByEmailSpecification(
        string email
    ) : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Email.ToLower() == email.ToLower();
        }
    }
}