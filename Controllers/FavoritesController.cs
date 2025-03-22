using AutoMapper;
using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;
using Bookly.APIs.Error;
using Bookly.APIs.Helpers;
using Bookly.APIs.Interfaces;
using Bookly.APIs.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookly.APIs.Controllers
{

    public class FavoritesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IFavoriteService favoriteService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _favoriteService = favoriteService;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination<Favorite>>>> GetFavorites([FromQuery]PaginationSpecParams specParams)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var favorites = await _favoriteService.GetFavoriteBooksAsync(user.Id,specParams);
            var countSpec = new FavoritesSpecifications(user.Id);
            var count = await _unitOfWork.Repository<Favorite>().GetCountWithSpecAsync(countSpec); 
            if (favorites is null) return NotFound(new ApiResponse(404));
            return Ok(new Pagination<Favorite>(specParams.PageIndex,specParams.PageSize,count,favorites));
        }

        [Authorize]
        [HttpPost("{bookId}")]
        public async Task<ActionResult<FavoriteToReturnDto>> AddFavoriteBook(int bookId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var FavoriteSpec = new FavoritesSpecifications(bookId, user.Id);
            var BookAddedToFavorites = await _unitOfWork.Repository<Favorite>().GetEntityWithSpecAsync(FavoriteSpec);
            if (BookAddedToFavorites is not null) return BadRequest(new ApiResponse(400, "Book is already added to your favorites list"));
            var spec = new BooksSpecification(bookId);
            var book = await _unitOfWork.Repository<Book>().GetEntityWithSpecAsync(spec);
            if (book is null) return NotFound(new ApiResponse(404, "Book not found"));


            var favorite = await _favoriteService.AddFavoriteBook(book, user.Id);
            if (favorite is null) return BadRequest(new ApiResponse(400));

            return Ok(favorite);

        }

        [Authorize]
        [HttpDelete("{bookId}")]
        public async Task<ActionResult<ActionDoneSuccessfullyMessageDto>> DeleteFavoriteBook(int bookid)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var isDeleted = await _favoriteService.DeleteFavoriteBookAsync(bookid, user.Id);
            if (isDeleted) return Ok(new ActionDoneSuccessfullyMessageDto("Book was deleted from favorites"));
            return BadRequest(new ApiResponse(400));
        }

    }
}
