using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class AuthorWithBooksSpecifications : BaseSpecifications<Author>
    {
        public AuthorWithBooksSpecifications(PaginationSpecParams specParams)
        {
            Includes.Add(A => A.Books);
            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        }

        public AuthorWithBooksSpecifications(int id) : base(A => A.Id == id)
        {
            Includes.Add(A => A.Books);
        }
    }
}
