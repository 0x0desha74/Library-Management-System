﻿using AutoMapper;
using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;
using Bookly.APIs.Error;
using Bookly.APIs.Helpers;
using Bookly.APIs.Interfaces;
using Bookly.APIs.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookly.APIs.Controllers
{

    public class AuthorsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination<AuthorToReturnDto>>>> GetAuthors([FromQuery] AuthorSpecParams specParams)
        {
            var spec = new AuthorWithBooksSpecifications(specParams);
            var authors = await _unitOfWork.Repository<Author>().GetAllWithSpecAsync(spec);
            var countSpec = new AuthorCountSpecifications();
            var count = await _unitOfWork.Repository<Author>().GetCountWithSpecAsync(countSpec);
            var data = _mapper.Map<IReadOnlyList<Author>, IReadOnlyList<AuthorToReturnDto>>(authors);
            return Ok(new Pagination<AuthorToReturnDto>(specParams.PageIndex,specParams.PageSize, count,data));
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorToReturnDto>> GetAuthor(int id)
        {
            var spec = new AuthorWithBooksSpecifications(id);
            var author = await _unitOfWork.Repository<Author>().GetEntityWithSpecAsync(spec);
            if (author is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Author, AuthorToReturnDto>(author));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AuthorToReturnDto>> Create(AuthorDto model)
        {
            var author = _mapper.Map<AuthorDto, Author>(model);
            await _unitOfWork.Repository<Author>().AddAsync(author);
            var result = await _unitOfWork.Complete();
            if (result == 0) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Author, AuthorToReturnDto>(author));

        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Author>> Edit(AuthorDto model)
        {
            var author = _mapper.Map<AuthorDto, Author>(model);
            _unitOfWork.Repository<Author>().Update(author);
            var result = await _unitOfWork.Complete();
            if (result == 0) return BadRequest(new ApiResponse(400));
            return Ok(author);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActionDoneSuccessfullyMessageDto>> Delete(int id)
        {
            var spec = new AuthorWithBooksSpecifications(id);
            var author = await _unitOfWork.Repository<Author>().GetEntityWithSpecAsync(spec);
            _unitOfWork.Repository<Author>().Delete(author);
            var result = await _unitOfWork.Complete();
            if (result == 0) return BadRequest(new ApiResponse(400));
            return Ok(new ActionDoneSuccessfullyMessageDto("Author was Deleted Successfully"));
        }

        [Authorize]
        //Add Book for a specific author
        [HttpPost("{authorId}/books")]
        public async Task<ActionResult<BookToReturnDto>> CreateBookForAuthor(int authorId, BookForAuthorDto model)
        {
            var spec = new AuthorWithBooksSpecifications(authorId);
            var author = await _unitOfWork.Repository<Author>().GetEntityWithSpecAsync(spec);
            var book = _mapper.Map<BookForAuthorDto, Book>(model);

            book.AuthorId = authorId;

            await _unitOfWork.Repository<Book>().AddAsync(book);
            var result = await _unitOfWork.Complete();
            if (result > 0) return Ok(_mapper.Map<Book, BookToReturnDto>(book));
            return BadRequest(new ApiResponse(400));
        }

        [Authorize]
        [HttpGet("{authorId}/books")]
        public async Task<ActionResult<IReadOnlyList<BookToReturnDto>>> GetBooksForAuthor(int authorId, [FromQuery] BooksSpecParams specParams)
        {
            var spec = new BooksOfAuthorSpecifications(authorId,specParams);
            var books = await _unitOfWork.Repository<Book>().GetAllWithSpecAsync(spec);
            var countSpec = new BooksOfAuthorSpecifications(authorId);
            var count = await _unitOfWork.Repository<Book>().GetCountWithSpecAsync(countSpec);
            if (books is null) return NotFound(new ApiResponse(404));
            var data = _mapper.Map<IReadOnlyList<Book>, IReadOnlyList<BookToReturnDto>>(books);
            return Ok(new Pagination<BookToReturnDto>(specParams.PageIndex,specParams.PageSize,count,data));
        }
        [Authorize]
        //Get book by id for specific author
        [HttpGet("{authorId}/books/{bookId}")]
        public async Task<ActionResult<BookToReturnDto>> GetBookForAuthor(int authorId, int bookId)
        {
            var spec = new BooksOfAuthorSpecifications(authorId, bookId);
            var book = await _unitOfWork.Repository<Book>().GetEntityWithSpecAsync(spec);
            if (book is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Book, BookToReturnDto>(book));
        }

        [Authorize(Roles="Admin")]
        //update book by id for a specific author
        [HttpPut("{authorId}/books/{bookId}")]
        public async Task<ActionResult<BookToReturnDto>> Edit(int authorId, int bookId, BookDto model)
        {
            var spec = new BooksOfAuthorSpecifications(authorId, bookId);
            var book = await _unitOfWork.Repository<Book>().GetEntityWithSpecAsync(spec);
            if (book is null) return NotFound(new ApiResponse(404));
            book.Title = model.Title;
            book.Description = model.Description;
            book.PublishedDate = model.PublishedDate;
            book.AvailableCount = model.AvailableCount;
            book.TotalCount = model.TotalCount;
            book.Genre = model.Genre;

            _unitOfWork.Repository<Book>().Update(book);
            var result = await _unitOfWork.Complete();
            if (result > 0)
                return Ok(_mapper.Map<Book, BookToReturnDto>(book));

            return BadRequest(new ApiResponse(400));
        }

        [Authorize(Roles="Admin")]
        [HttpDelete("{authorId}/books/{bookId}")]
        public async Task<ActionResult<ActionDoneSuccessfullyMessageDto>> Delete(int authorId, int bookId)
        {
            var spec = new BooksOfAuthorSpecifications(authorId, bookId);
            var book = await _unitOfWork.Repository<Book>().GetEntityWithSpecAsync(spec);
            if (book is null) return NotFound(new ApiResponse(404));
            _unitOfWork.Repository<Book>().Delete(book);
            var result = await _unitOfWork.Complete();
            if (result > 0) return Ok(new ActionDoneSuccessfullyMessageDto("Book was Deleted Successfully"));
            return BadRequest(new ApiResponse(400));
        }


    }
}
