using Bookly.APIs.Entities;
using System.Linq.Expressions;

namespace Bookly.APIs.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>> Criteria { get; set; }
        List<Expression<Func<T, object>>> Includes { get; set; }
        int Skip { get; set; }
        int Take { get; set; }
        bool IsPaginationEnabled { get; set; }

    }
}
