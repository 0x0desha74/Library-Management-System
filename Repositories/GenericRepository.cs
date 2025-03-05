using Bookly.APIs.Data;
using Bookly.APIs.Entities;
using Bookly.APIs.Interfaces;
using Bookly.APIs.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Bookly.APIs.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            throw new NotImplementedException();
        }







        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            
        }


        public void UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
