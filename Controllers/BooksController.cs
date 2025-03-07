using AutoMapper;
using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;
using Bookly.APIs.Error;
using Bookly.APIs.Interfaces;
using Bookly.APIs.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace Bookly.APIs.Controllers
{

    public class BooksController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BooksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Book>>> GetBooks()
        {
            var spec = new BookWithAuthorsSpecification();
            var books = await _unitOfWork.Repository<Book>().GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Book>, IReadOnlyList<BookToReturnDto>>(books));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var spec = new BookWithAuthorsSpecification(id);
            var book = await _unitOfWork.Repository<Book>().GetEntityWithSpecAsync(spec);
            return book is not null ? Ok(_mapper.Map<Book,BookToReturnDto>(book)) : NotFound(new ApiResponse(404));
        }


        [HttpPost]
        public async Task<ActionResult<BookDto>> Create(BookDto model)
        {
            var mappedBook = _mapper.Map<BookDto, Book>(model);
            await _unitOfWork.Repository<Book>().AddAsync(mappedBook);
            await _unitOfWork.Complete();
            return Ok(model);
        }

        [HttpPut]
        public async Task<ActionResult<BookDto>> Edit(BookDto model)
        {
           var mappedBook= _mapper.Map<BookDto, Book>(model);
            _unitOfWork.Repository<Book>().Update(mappedBook);
            await _unitOfWork.Complete();
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeletedMessageDto>> Delete(int id)
        {
            var book = await _unitOfWork.Repository<Book>().GetByIdAsync(id);
            _unitOfWork.Repository<Book>().Delete(book);
            await _unitOfWork.Complete();
            return Ok(new DeletedMessageDto("Book is deleted successfully"));
        }


    }
}
