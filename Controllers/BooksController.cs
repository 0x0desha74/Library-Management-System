using Bookly.APIs.Entities;
using Bookly.APIs.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookly.APIs.Controllers
{

    public class BooksController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Book>>> GetBooks()
        {
            var books = await _unitOfWork.Repository<Book>().GetAllAsync();
            return Ok(books);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _unitOfWork.Repository<Book>().GetByIdAsync(id);
            return book is not null ? Ok(book) : NotFound();
        }


    }
}
