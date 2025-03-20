using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class ReviewSpecifications : BaseSpecifications<Review>
    {
        public ReviewSpecifications(int id) : base(r => r.BookId == id)
        {

        }
    }
}
