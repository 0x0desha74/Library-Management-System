using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;

namespace Bookly.APIs.Interfaces
{
    public interface IBorrowService
    {
        Task<BorrowRecord?> ReturnBook(int bookId, string userId);
    }
}
