

using Bookly.APIs.Entities;
using Bookly.APIs.Specifications;

namespace Bookly.APIs.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task AddAsync(T Entity);
        void UpdateAsync(int id);
        void DeleteAsync(int id);

    }
}
