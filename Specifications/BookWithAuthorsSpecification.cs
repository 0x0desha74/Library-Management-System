using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class BookWithAuthorsSpecification : BaseSpecifications<Book>
    {
        public BookWithAuthorsSpecification()
        {
            Includes.Add(b => b.Author);

        }
        public BookWithAuthorsSpecification(int id) : base(b => b.Id == id)
        {
            Includes.Add(b => b.Author);
        }


    }
}
