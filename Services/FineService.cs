using AutoMapper;
using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;
using Bookly.APIs.Interfaces;
using Bookly.APIs.Specifications;

namespace Bookly.APIs.Services
{
    public class FineService : IFineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FineService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Fine?> CreateFineAsync(int bookId, FineDto model)
        {
            var spec = new BorrowRecordSpecifications(model.UserId, bookId);
            var record = await _unitOfWork.Repository<BorrowRecord>().GetEntityWithSpecAsync(spec);
            if (record is null) return null;

            var fine = _mapper.Map<FineDto, Fine>(model);
            fine.BorrowRecordId = record.Id;
            fine.BookId = record.BookId;
            await _unitOfWork.Repository<Fine>().AddAsync(fine);
            var result = await _unitOfWork.Complete();

            return result > 0 ? fine : null;
        }

        public async Task<Fine?> PayFineAsync(int fineId)
        {
            var fine = await _unitOfWork.Repository<Fine>().GetByIdAsync(fineId);
            if (fine is null) return null;
            if (fine.IsPaid) throw new InvalidOperationException("Fine is already paid");
            fine.IsPaid = true;
            var result = await _unitOfWork.Complete();
            return result > 0 ? fine : null;
        }


    }
}
