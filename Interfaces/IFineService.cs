using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;

namespace Bookly.APIs.Interfaces
{
    public interface IFineService
    {
        Task<Fine?> CreateFineAsync(int bookId, FineDto model);
        Task<Fine?> PayFineAsync(int fineId);
    }
}
