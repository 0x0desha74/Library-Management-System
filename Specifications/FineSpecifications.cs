using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class FineSpecifications:BaseSpecifications<Fine>
    {
        public FineSpecifications(int bookId,int fineId):base(f=>f.Id ==fineId && f.BookId==bookId)
        {

        }
    }
}
