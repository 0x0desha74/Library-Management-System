using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class BooksOfAuthorSpecifications : BaseSpecifications<Book>
    {
        public BooksOfAuthorSpecifications(int authorId, BooksSpecParams specParams)
            : base(
                  b =>
                    (string.IsNullOrEmpty(specParams.Search) || b.Title.ToLower().Contains(specParams.Search)) &&

                  b.AuthorId == authorId
                  )
        {
            Includes.Add(b => b.Author);
            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        }

        public BooksOfAuthorSpecifications(int authorId, int bookId) : base(b => b.Id == bookId && b.AuthorId == authorId)
        {
            Includes.Add(b => b.Author);
        }
        public BooksOfAuthorSpecifications(int authorId) : base(b => b.AuthorId == authorId)
        {

        }
    }
}
