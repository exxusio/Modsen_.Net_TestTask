using System.Linq.Expressions;

namespace EventsWebApplication.Domain.Specifications
{
    public abstract class Specification<TEntity> : ISpecification<TEntity>
    {
        public bool IsSatisfiedBy(TEntity entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<TEntity, bool>> ToExpression();
    }
}