using System.Linq.Expressions;
using EventsWebApplication.Domain.Entities.Interfaces;
using EventsWebApplication.Domain.Specifications;

namespace EventsWebApplication.Infrastructure.Specifications.Bases
{
    public class ByNameSpecification<TEntity>(
        string name)
        : Specification<TEntity>
        where TEntity : class, IHaveName
    {
        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return entity => entity.Name == name;
        }
    }
}
