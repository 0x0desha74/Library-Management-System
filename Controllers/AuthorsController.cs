using AutoMapper;
using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;
using Bookly.APIs.Error;
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
            if (author is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Author, AuthorToReturnDto>(author));
        }


        [HttpPost]
        public async Task<ActionResult<AuthorToReturnDto>> Create(AuthorDto model)
        {
            var author = _mapper.Map<AuthorDto, Author>(model);
            await _unitOfWork.Repository<Author>().AddAsync(author);
           var result =  await _unitOfWork.Complete();
            if (result == 0) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Author,AuthorToReturnDto>(author));

        }

        [HttpPut]
        public async Task<ActionResult<Author>> Edit(AuthorDto model)
        {
            var author = _mapper.Map<AuthorDto, Author>(model);
            _unitOfWork.Repository<Author>().Update(author);
            var result = await _unitOfWork.Complete();
            if (result == 0) return BadRequest(new ApiResponse(400));
            return Ok(author);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeletedMessageDto>> Delete(int id)
        {
            var spec = new AuthorWithSpecSpecifications(id);
            var author = await _unitOfWork.Repository<Author>().GetEntityWithSpecAsync(spec);
            _unitOfWork.Repository<Author>().Delete(author);
            var result = await _unitOfWork.Complete();
            if (result == 0) return BadRequest(new ApiResponse(400));
            return Ok(new DeletedMessageDto("Author was Deleted Successfully"));
            
        }

    }
}
