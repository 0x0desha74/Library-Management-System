using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class FavoritesSpecifications:BaseSpecifications<Favorite>
    {
        public FavoritesSpecifications(string userId):base(f=>f.UserId== userId)
        {
            //Includes.Add(f => f.Book);
        }
        public FavoritesSpecifications(int bookId,string userId) : base(f => f.UserId == userId & f.bookId == bookId)
        {
            //Includes.Add(f => f.Book);
        }
    }
}
