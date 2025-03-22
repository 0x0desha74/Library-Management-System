using Bookly.APIs.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookly.APIs.Specifications
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);


            if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.Top > 0)
                query = query.Take(spec.Top);

            //if (spec.OrderByDescending is not null && spec.Top > 0)
            //{
            //    query = query.OrderByDescending(spec.OrderByDescending);
            //    query = query.Take(spec.Top);
            //}
            //else
            //    query = query.OrderByDescending(spec.OrderByDescending);






            if (spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            



            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));


            return query;
        }
    }
}
