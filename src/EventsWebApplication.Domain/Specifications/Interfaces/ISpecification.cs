using System.Linq.Expressions;

namespace EventsWebApplication.Domain.Specifications.Interfaces
{
    public interface ISpecification<TEntity>
    {
        bool IsSatisfiedBy(TEntity entity);
        Expression<Func<TEntity, bool>> ToExpression();
    }
}