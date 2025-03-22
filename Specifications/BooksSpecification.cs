using Bookly.APIs.Entities;
using Microsoft.EntityFrameworkCore.Query;
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

            AddOrderByAsc(b => b.Title);

            //if (specParams.Top.HasValue)
            //{
            //    Includes.Add(b => b.Favorites);
            //    AddOrderByDescending(b => b.Favorites.Count);
            //    Top = specParams.Top.Value;
            //}
            if (!string.IsNullOrEmpty(specParams.sort))
            {

                switch (specParams.sort)
                {
                    case "Desc":
                        AddOrderByDescending(b => b.Title);
                        break;
                    default:
                        AddOrderByAsc(b => b.Title);
                        break;
                }
            }

           

            ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
        public BooksSpecification(int id) : base(b => b.Id == id)
        {
            Includes.Add(b => b.Author);
            Includes.Add(b => b.Reviews);

        }

      

    }
}
