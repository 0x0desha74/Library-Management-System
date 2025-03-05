using Bookly.APIs.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookly.APIs.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Book> Books{ get; set; }
        public DbSet<Author> Authors{ get; set; }
    }
}
