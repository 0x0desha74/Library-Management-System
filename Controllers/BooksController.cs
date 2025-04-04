﻿using AutoMapper;
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

    public class BooksController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFineService _fineService;
        private readonly IBorrowService _borrowService;
        public BooksController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IFineService fineService, IBorrowService borrowService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _fineService = fineService;
            _borrowService = borrowService;
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination<BookToReturnDto>>>> GetBooks([FromQuery] BooksSpecParams specParams)
        {
            var spec = new BooksSpecification(specParams);
            var countSpec = new BooksWithFilterationForCountSpecification(specParams);
            var books = await _unitOfWork.Repository<Book>().GetAllWithSpecAsync(spec);
            var count = await _unitOfWork.Repository<Book>().GetCountWithSpecAsync(countSpec);
            var data = _mapper.Map<IReadOnlyList<Book>, IReadOnlyList<BookToReturnDto>>(books);
            return Ok(new Pagination<BookToReturnDto>(specParams.PageIndex, specParams.PageSize, count, data));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var spec = new BooksSpecification(id);
            var book = await _unitOfWork.Repository<Book>().GetEntityWithSpecAsync(spec);
            return book is not null ? Ok(_mapper.Map<Book, BookToReturnDto>(book)) : NotFound(new ApiResponse(404));
        }

        [HttpGet("top")]
        public async Task<ActionResult<IReadOnlyList<Pagination<BookToReturnDto>>>> GetTopBooks(int top,[FromQuery]PaginationSpecParams specParams)
        {
            var spec = new TopBooksSpecifications(top);
            var TopBooks = await _unitOfWork.Repository<Book>().GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Book>, IReadOnlyList<BookToReturnDto>>(TopBooks);
            if (TopBooks is null) return NotFound(new ApiResponse(404));
            return Ok(new Pagination<BookToReturnDto>(specParams.PageIndex, specParams.PageSize, TopBooks.Count, data));
        }




        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<BookDto>> Create(BookDto model)
        {
            var mappedBook = _mapper.Map<BookDto, Book>(model);
            await _unitOfWork.Repository<Book>().AddAsync(mappedBook);
            await _unitOfWork.Complete();
            return Ok(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<BookDto>> Edit(BookDto model)
        {
            var mappedBook = _mapper.Map<BookDto, Book>(model);
            _unitOfWork.Repository<Book>().Update(mappedBook);
            await _unitOfWork.Complete();
            return Ok(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActionDoneSuccessfullyMessageDto>> Delete(int id)
        {
            var book = await _unitOfWork.Repository<Book>().GetByIdAsync(id);
            _unitOfWork.Repository<Book>().Delete(book);
            await _unitOfWork.Complete();
            return Ok(new ActionDoneSuccessfullyMessageDto("Book is deleted successfully"));
        }

        [Authorize]
        [HttpGet("{id}/reviews")]
        public async Task<ActionResult<IReadOnlyList<Pagination<Review>>>> GetReviews(int id, [FromQuery] PaginationSpecParams specParams)
        {
            var spec = new ReviewSpecifications(id, specParams);
            var reviews = await _unitOfWork.Repository<Review>().GetAllWithSpecAsync(spec);
            var count = await _unitOfWork.Repository<Review>().GetCountWithSpecAsync(spec);
            if (reviews is null) return NotFound(new ApiResponse(404));
            return Ok(new Pagination<Review>(specParams.PageIndex, specParams.PageSize, count, reviews));
        }

        [Authorize]
        [HttpPost("{bookId}/reviews")]
        public async Task<ActionResult<Review>> CreateReview(int bookId, ReviewDto model)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var review = _mapper.Map<ReviewDto, Review>(model);
            review.BookId = bookId;
            review.UserId = user.Id;
            review.ReviewerName = user.DisplayName;
            await _unitOfWork.Repository<Review>().AddAsync(review);
            var result = await _unitOfWork.Complete();
            if (result > 0) return Ok(review);
            return BadRequest(new ApiResponse(400));
        }

        [Authorize]
        [HttpDelete("{bookId}/reviews/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _unitOfWork.Repository<Review>().GetByIdAsync(id);
            _unitOfWork.Repository<Review>().Delete(review);
            var result = await _unitOfWork.Complete();
            if (result > 0) return NoContent();
            return BadRequest(new ApiResponse(400));
        }

        [Authorize]
        [HttpPost("{bookId}/borrow")]
        public async Task<ActionResult<BorrowRecord>> Borrow(int bookId, BorrowRecordDto model)
        {
            var book = await _unitOfWork.Repository<Book>().GetByIdAsync(model.BookId);
            if (book.AvailableCount == 0) return BadRequest(new ApiResponse(400, "This Book Is Not Available Right Now"));
            var record = await _borrowService.BorrowBook(book, model);
            if (record is null) return NotFound(new ApiResponse(404));
            return Ok(record);

        }

        [Authorize]
        [HttpPost("{bookId}/return")]
        public async Task<ActionResult<BorrowRecordToReturnDto>> Return(int bookId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var record = await _borrowService.ReturnBook(bookId, user.Id);
            if (record is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<BorrowRecord, BorrowRecordToReturnDto>(record));


        }


        [HttpGet("{bookId}/borrow-status")]
        public async Task<ActionResult<ActionDoneSuccessfullyMessageDto>> BorrowStatus(int bookId)
        {
            var spec = new BorrowRecordSpecifications(bookId);
            var record = await _unitOfWork.Repository<BorrowRecord>().GetEntityWithSpecAsync(spec);
            if (record is null) return NotFound(new ApiResponse(404, "No borrow records for this book"));
            if (record.IsReturned) return Ok(new ActionDoneSuccessfullyMessageDto("Book is borrowed"));
            return Ok(new ActionDoneSuccessfullyMessageDto("Book is available"));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{bookId}/borrow-records")]
        public async Task<ActionResult<IReadOnlyList<Pagination<BorrowRecordToReturnDto>>>> GetBorrowRecords(int bookId, [FromQuery] PaginationSpecParams specParams)
        {
            var spec = new BorrowRecordSpecifications(bookId, specParams);
            var countSpec = new BorrowRecordsCountSpecification(bookId);
            var records = await _unitOfWork.Repository<BorrowRecord>().GetAllWithSpecAsync(spec);
            var count = await _unitOfWork.Repository<BorrowRecord>().GetCountWithSpecAsync(countSpec);
            if (records is null) return BadRequest(new ApiResponse(404));
            var data = _mapper.Map<IReadOnlyList<BorrowRecord>, IReadOnlyList<BorrowRecordToReturnDto>>(records);
            return Ok(new Pagination<BorrowRecordToReturnDto>(specParams.PageIndex, specParams.PageSize, count, data));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{bookId}/fines")]
        public async Task<ActionResult<IReadOnlyList<Pagination<FineToReturnDto>>>> GetFines(int bookId, [FromQuery] PaginationSpecParams specParams)
        {
            var spec = new FineSpecifications(bookId, specParams);
            var fines = await _unitOfWork.Repository<Fine>().GetAllWithSpecAsync(spec);
            var count = await _unitOfWork.Repository<Fine>().GetCountWithSpecAsync(spec);
            if (fines is null) return NotFound(new ApiResponse(404, "No fines found for this book"));
            var data = _mapper.Map<IReadOnlyList<Fine>, IReadOnlyList<FineToReturnDto>>(fines);
            return Ok(new Pagination<FineToReturnDto>(specParams.PageIndex,specParams.PageSize,count,data));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("{bookId}/fines")]
        public async Task<ActionResult<Fine>> CreateFine(int bookId, FineDto model)
        {
            var fine = await _fineService.CreateFineAsync(bookId, model);
            if (fine is null) return NotFound(new ApiResponse(404));
            return Ok(fine);
        }


        [Authorize]
        [HttpPost("{bookId}/fines/{findId}/pay")]
        public async Task<ActionResult<Fine>> PayFine(int findId)
        {
            var fine = await _fineService.PayFineAsync(findId);
            if (fine is null) return NotFound(new ApiResponse(404, "Fine not found"));
            return Ok(fine);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{bookId}/fines/{fineId}")]
        public async Task<ActionResult<ActionDoneSuccessfullyMessageDto>> DeleteFine(int fineId)
        {
            var fine = await _unitOfWork.Repository<Fine>().GetByIdAsync(fineId);
            if (fine is null) return NotFound(new ApiResponse(404, "No Fines For this book"));
            _unitOfWork.Repository<Fine>().Delete(fine);
            var result = await _unitOfWork.Complete();
            if (result > 0) return Ok(new ActionDoneSuccessfullyMessageDto("Fine deleted successfully"));
            return BadRequest(new ApiResponse(400));
        }




    }
}
