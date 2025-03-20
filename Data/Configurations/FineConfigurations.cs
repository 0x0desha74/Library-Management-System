using Bookly.APIs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookly.APIs.Data.Configurations
{
    public class FineConfigurations : IEntityTypeConfiguration<Fine>
    {
        public void Configure(EntityTypeBuilder<Fine> builder)
        {
            builder.Property(f => f.Amount)
                .HasColumnType("decimal(18,2)");


        }
    }
}
