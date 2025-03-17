using Bookly.APIs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookly.APIs.Data.Configurations
{
    public class BorrowRecordConfigurations : IEntityTypeConfiguration<BorrowRecord>
    {
        public void Configure(EntityTypeBuilder<BorrowRecord> builder)
        {
            builder.HasOne(br => br.Fine)
                .WithOne()
                .HasForeignKey<Fine>(f => f.BorrowRecordId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
