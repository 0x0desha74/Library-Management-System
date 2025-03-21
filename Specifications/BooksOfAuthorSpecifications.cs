using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class BooksOfAuthorSpecifications : BaseSpecifications<Book>
    {
        public BooksOfAuthorSpecifications(int authorId,PaginationSpecParams specParams) : base(b => b.AuthorId == authorId)
        {
            Includes.Add(b => b.Author);
            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        }

        public BooksOfAuthorSpecifications(int authorId, int bookId) : base(b => b.Id == bookId && b.AuthorId == authorId)
        {
            Includes.Add(b => b.Author);
        }
    }
}
