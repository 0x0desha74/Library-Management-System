using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class ReviewSpecifications : BaseSpecifications<Review>
    {
        public ReviewSpecifications(int id, PaginationSpecParams specParams) : base(r => r.BookId == id)
        {
            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        }
    }
}
