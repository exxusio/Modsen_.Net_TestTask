using System.Linq.Expressions;

namespace EventsWebApplication.Domain.Specifications
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() => param => true;
        public static Expression<Func<T, bool>> False<T>() => param => false;

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.AndAlso(
                Expression.Invoke(first, parameter),
                Expression.Invoke(second, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.OrElse(
                Expression.Invoke(first, parameter),
                Expression.Invoke(second, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}