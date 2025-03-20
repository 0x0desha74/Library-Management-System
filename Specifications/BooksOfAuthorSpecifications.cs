using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class BooksOfAuthorSpecifications : BaseSpecifications<Book>
    {
        public BooksOfAuthorSpecifications(int authorId) : base(b => b.AuthorId == authorId)
        {
            Includes.Add(b => b.Author);
        }

        public BooksOfAuthorSpecifications(int authorId, int bookId) : base(b => b.Id == bookId && b.AuthorId == authorId)
        {
            Includes.Add(b => b.Author);
        }
    }
}
