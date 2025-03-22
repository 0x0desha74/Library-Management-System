using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class BorrowRecordsCountSpecification:BaseSpecifications<BorrowRecord>
    {
        public BorrowRecordsCountSpecification(int bookId) : base(br => br.BookId == bookId)
        {
            

        }
    }
}
