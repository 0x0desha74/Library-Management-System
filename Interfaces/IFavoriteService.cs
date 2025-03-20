using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;

namespace Bookly.APIs.Interfaces
{
    public interface IFavoriteService
    {
        Task<IReadOnlyList<Favorite>> GetFavoriteBooksAsync(string userId);
        Task<FavoriteToReturnDto> AddFavoriteBook(Book book,string userId);
        Task<bool> DeleteFavoriteBookAsync(int bookId,string userId);
    }
}
