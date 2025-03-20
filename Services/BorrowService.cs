using AutoMapper;
using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;
using Bookly.APIs.Interfaces;
using Bookly.APIs.Specifications;

namespace Bookly.APIs.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BorrowService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BorrowRecord?> BorrowBook(Book book, BorrowRecordDto model)
        {

            var record = _mapper.Map<BorrowRecordDto, BorrowRecord>(model);
            record.BookId = model.BookId;
            await _unitOfWork.Repository<BorrowRecord>().AddAsync(record);
            book.AvailableCount -= 1;
            var result = await _unitOfWork.Complete();
            return result > 0 ? record : null;
        }

        public async Task<BorrowRecord?> ReturnBook(int bookId, string userId)
        {
            var spec = new BorrowRecordSpecifications(userId, bookId);
            var record = await _unitOfWork.Repository<BorrowRecord>().GetEntityWithSpecAsync(spec);

            if (record is null) return null;
            if (record.IsReturned) throw new InvalidOperationException("Book is already returned");
            record.ReturnDate = DateTime.Now;
            record.IsReturned = true;
            var book = await _unitOfWork.Repository<Book>().GetByIdAsync(bookId);
            book.AvailableCount += 1;

            var result = await _unitOfWork.Complete();
            return result > 0 ? record : null;
        }
    }
}
