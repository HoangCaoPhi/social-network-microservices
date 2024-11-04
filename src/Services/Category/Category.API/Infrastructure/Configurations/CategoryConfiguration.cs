using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Category.API.Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Models.Category>
{
    public void Configure(EntityTypeBuilder<Models.Category> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(255);

        builder
            .Property(o => o.Status)
            .HasConversion<string>()
            .HasMaxLength(30);
    }
}
