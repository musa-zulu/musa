using B2B.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2B.Infrastructure.Products.Persistence;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(r => r.Id)
               .ValueGeneratedNever();

        builder.HasIndex(o => o.Name);
        builder.Property(o => o.SKU);
        builder.Property(o => o.UnitPrice);
        builder.Property(o => o.AvailableQuantity);
        builder.Property(o => o.Category);

        builder.Property(o => o.RowVersion)
            .IsRowVersion();
    }
}