using Bookly.APIs.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bookly.APIs.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Book> Books{ get; set; }
        public DbSet<Author> Authors{ get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
        public DbSet<Fine> Fines{ get; set; }
    }
}
