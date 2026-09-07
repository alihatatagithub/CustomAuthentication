using System.Linq.Expressions;

namespace ECommerce.Ground
{
    public static class QueryExtensions
    {
        public static IQueryable<TEntity> FilterIf<TEntity>(this IQueryable<TEntity> query, bool ifCondition, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            if (ifCondition)
                return query.Where(predicate);
            else
                return query;
        }
    }
}
