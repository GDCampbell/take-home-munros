using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using TakeHomeMunrosApi.Queries;

namespace TakeHomeMunrosApi.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderBySortingCriterias<T>(this IQueryable<T> source, IList<ISortingCriteria> sortingCriterias)
        {
            if (!sortingCriterias.Any())
            {
                return source;
            }

            var result = source;
            var isNested = false;
            foreach (var sortingCriteria in sortingCriterias)
            {
                result = OrderBySortingCriteria(result, sortingCriteria, isNested);
                isNested = true;
            }

            return result;
        }

        // Took inspiration from here on how to accomplish this
        // https://www.c-sharpcorner.com/article/dynamic-sorting-orderby-based-on-user-preference/

        public static IQueryable<T> OrderBySortingCriteria<T>(this IQueryable<T> source, ISortingCriteria sortingCriteria, bool isNested = false)
        {
            if (string.IsNullOrEmpty(sortingCriteria.PropertyName))
            {
                return source;
            }

            var parameter = Expression.Parameter(source.ElementType, "");
            var property = Expression.Property(parameter, sortingCriteria.PropertyName);
            var lambda = Expression.Lambda(property, parameter);
            
            var methodName = $"{(!isNested ? "Order" : "Then")}By{(sortingCriteria.IsAscending ? "" : "Descending")}";

            var methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                new [] {source.ElementType, property.Type},
                source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}
