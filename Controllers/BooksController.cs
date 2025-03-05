using AutoMapper;
using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;
using Bookly.APIs.Interfaces;
using Bookly.APIs.Specifications;
using Microsoft.AspNetCore.Http;
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
            return book is not null ? Ok(_mapper.Map<Book,BookToReturnDto>(book)) : NotFound();
        }


    }
}
