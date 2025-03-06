using AutoMapper;
using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;
using Bookly.APIs.Interfaces;
using Bookly.APIs.Specifications;
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
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AuthorToReturnDto>>> GetAuthors()
        {
            var spec = new AuthorWithSpecSpecifications();
            var authors = await _unitOfWork.Repository<Author>().GetAllWithSpecAsync(spec);
            var mappedAuthors = _mapper.Map<IReadOnlyList<Author>, IReadOnlyList<AuthorToReturnDto>>(authors);
            return Ok(mappedAuthors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorToReturnDto>> GetAuthor(int id)
        {
            var spec = new AuthorWithSpecSpecifications(id);
            var author = await _unitOfWork.Repository<Author>().GetEntityWithSpecAsync(spec);
            if (author is null) return NotFound();
            return Ok(_mapper.Map<Author, AuthorToReturnDto>(author));
        }
    }
}
