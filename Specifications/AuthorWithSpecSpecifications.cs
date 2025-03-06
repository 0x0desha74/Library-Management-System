using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class AuthorWithSpecSpecifications : BaseSpecifications<Author>
    {
        public AuthorWithSpecSpecifications()
        {
            Includes.Add(A => A.Books);
        }

        public AuthorWithSpecSpecifications(int id):base(A=>A.Id==id)
        {
            Includes.Add(A => A.Books);
        }
    }
}
