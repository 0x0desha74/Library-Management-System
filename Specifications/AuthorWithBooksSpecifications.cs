using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class AuthorWithBooksSpecifications : BaseSpecifications<Author>
    {
        public AuthorWithBooksSpecifications()
        {
            Includes.Add(A => A.Books);
        }

        public AuthorWithBooksSpecifications(int id) : base(A => A.Id == id)
        {
            Includes.Add(A => A.Books);
        }
    }
}
