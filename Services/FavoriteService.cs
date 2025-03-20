using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;
using Bookly.APIs.Interfaces;
using Bookly.APIs.Specifications;

namespace Bookly.APIs.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FavoriteToReturnDto> AddFavoriteBook(Book book, string userId)
        {
            var  favorite = new Favorite()
            {
                bookId = book.Id,
                UserId = userId
            };

           await  _unitOfWork.Repository<Favorite>().AddAsync(favorite);
            var result = await _unitOfWork.Complete();
            if (result > 0)
            {
               
                var mappedFavorite = new FavoriteToReturnDto()
                {
                    BookId = favorite.bookId,
                    CreateAt = favorite.CreatedAt,
                    Author = book.Author.Name,
                    Title = book.Title
                };
            return mappedFavorite;
            }
            return null;
        }

        public async Task<bool> DeleteFavoriteBookAsync(int bookId, string userId)
        {
            var spec = new FavoritesSpecifications(bookId, userId);
            var favorite = await _unitOfWork.Repository<Favorite>().GetEntityWithSpecAsync(spec);
            if (favorite is null) return false;
            _unitOfWork.Repository<Favorite>().Delete(favorite);
            var result = await _unitOfWork.Complete();
            return result > 0 ? true : false;
        }

        public async Task<IReadOnlyList<Favorite>> GetFavoriteBooksAsync(string userId)
        {

            var spec = new FavoritesSpecifications(userId);
            var favorites = await _unitOfWork.Repository<Favorite>().GetAllWithSpecAsync(spec);
            if (favorites is null) return null;
            return favorites;
        }
    }
}
