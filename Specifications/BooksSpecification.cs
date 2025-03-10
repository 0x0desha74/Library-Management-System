using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class BooksSpecification : BaseSpecifications<Book>
    {
        public BooksSpecification(int? authorId, string? genre)
            : base(b =>
                (authorId == null || (b.Author != null && b.Author.Id == authorId.Value)) &&
        (string.IsNullOrEmpty(genre) || (b.Genre != null && b.Genre == genre))
            )
        {
            Includes.Add(b => b.Author);
            Includes.Add(b => b.Reviews);

        }
        public BooksSpecification(int id) : base(b => b.Id == id)
        {
            Includes.Add(b => b.Author);
            Includes.Add(b => b.Reviews);

        }


    }
}
