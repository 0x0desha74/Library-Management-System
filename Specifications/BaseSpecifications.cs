using Bookly.APIs.Entities;
using System.Linq.Expressions;

namespace Bookly.APIs.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
        public Expression<Func<T,object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int Top { get; set; }
        public BaseSpecifications()
        {
            
        }

        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;

        }

        public void AddOrderByAsc(Expression<Func<T, object>> orderByAscExpression)
        {
            OrderBy = orderByAscExpression;
        }

        public void ApplyTopRecords(int top,Expression<Func<T,object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
            Top = top;
            ApplyPagination(0, top);
        }



        public void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
        public void ApplyPagination(int skip,int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
