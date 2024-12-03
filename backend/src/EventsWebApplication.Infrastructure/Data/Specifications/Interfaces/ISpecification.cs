using System.Linq.Expressions;

namespace EventsWebApplication.Infrastructure.Data.Specifications.Interfaces
{
    public interface ISpecification<TEntity>
    {
        bool IsSatisfiedBy(TEntity entity);
        Expression<Func<TEntity, bool>> ToExpression();
    }
}