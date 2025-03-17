using Bookly.APIs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookly.APIs.Data.Configurations
{
    public class BookConfigurations : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasOne(b => b.Author)
                .WithMany(A => A.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.Reviews)
                .WithOne()
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.BorrowRecords)
              .WithOne()
              .HasForeignKey(br => br.BookId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
