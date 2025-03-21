using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;
using Bookly.APIs.Specifications;

namespace Bookly.APIs.Interfaces
{
    public interface IFavoriteService
    {
        Task<IReadOnlyList<Favorite>> GetFavoriteBooksAsync(string userId,PaginationSpecParams specParams);
        Task<FavoriteToReturnDto> AddFavoriteBook(Book book, string userId);
        Task<bool> DeleteFavoriteBookAsync(int bookId, string userId);
    }
}
