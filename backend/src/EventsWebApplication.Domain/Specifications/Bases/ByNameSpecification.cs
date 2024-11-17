using System.Linq.Expressions;
using EventsWebApplication.Domain.Entities.Interfaces;

namespace EventsWebApplication.Domain.Specifications.Bases
{
    public abstract class ByNameSpecification<TEntity>(
        string name
    ) : Specification<TEntity>
    where TEntity : class, IHaveName
    {
        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return entity => entity.Name.ToLower() == name.ToLower();
        }
    }
}
