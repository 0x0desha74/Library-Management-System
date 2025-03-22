using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class BooksWithFilterationForCountSpecification:BaseSpecifications<Book>
    {
        public BooksWithFilterationForCountSpecification(BooksSpecParams specParams)
             : base(b =>
                (specParams.AuthorId == null || (b.Author != null && b.Author.Id == specParams.AuthorId.Value)) &&
        (string.IsNullOrEmpty(specParams.Genre) || (b.Genre != null && b.Genre == specParams.Genre))
            )
        {

        }
        
    }
}
