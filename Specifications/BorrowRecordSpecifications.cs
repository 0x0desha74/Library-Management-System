using Bookly.APIs.Entities;

namespace Bookly.APIs.Specifications
{
    public class BorrowRecordSpecifications:BaseSpecifications<BorrowRecord>
    {
        public BorrowRecordSpecifications(string userId,int bookId):base(br=>br.UserId==userId && br.BookId==bookId)
        {
            Includes.Add(br => br.Fine);

        }

        public BorrowRecordSpecifications(int bookId) : base(br => br.BookId==bookId)
        {
            Includes.Add(br => br.Fine);

        }
    }
}
