using Bookly.APIs.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Bookly.APIs.Specifications
{
    public class BooksSpecification : BaseSpecifications<Book>
    {
        public BooksSpecification(BooksSpecParams specParams)
            : base(b =>
                (specParams.AuthorId == null || (b.Author != null && b.Author.Id == specParams.AuthorId.Value)) &&
        (string.IsNullOrEmpty(specParams.Genre) || (b.Genre != null && b.Genre == specParams.Genre))
            )
        {
            Includes.Add(b => b.Author);
            Includes.Add(b => b.Reviews);
            ApplyPagination(specParams.PageSize *(specParams.PageIndex - 1) , specParams.PageSize);
        }
        public BooksSpecification(int id) : base(b => b.Id == id)
        {
            Includes.Add(b => b.Author);
            Includes.Add(b => b.Reviews);

        }


    }
}
