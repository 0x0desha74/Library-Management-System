﻿using Bookly.APIs.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookly.APIs.Specifications
{
    public static class SpecificationEvaluator<T> where T:BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
                query.Where(spec.Criteria);






            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            return query;
        }
    }
}
