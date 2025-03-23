using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class AuthorWithBooksSpecifications : BaseSpecifications<Author>
    {
        public AuthorWithBooksSpecifications(AuthorSpecParams specParams):base(a => (string.IsNullOrEmpty(specParams.Search) || a.Name.ToLower().Contains(specParams.Search)) 
)
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
