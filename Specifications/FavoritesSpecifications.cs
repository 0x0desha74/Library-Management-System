using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class FavoritesSpecifications : BaseSpecifications<Favorite>
    {
        public FavoritesSpecifications(string userId,PaginationSpecParams specParams) : base(f => f.UserId == userId)
        {
            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
            //Includes.Add(f => f.Book);
        }
        public FavoritesSpecifications(int bookId, string userId) : base(f => f.UserId == userId & f.BookId == bookId)
        {
            //Includes.Add(f => f.Book);
        }
    }
}
