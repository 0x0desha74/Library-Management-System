using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class TopBooksSpecifications:BaseSpecifications<Book>
    {
        public TopBooksSpecifications(int top)
        {
            Includes.Add(b => b.Author);
            Includes.Add(b => b.Favorites);
            Includes.Add(b => b.BorrowRecords);
            ApplyTopRecords(top,b=>b.Favorites.Count);
        }
    }
}
