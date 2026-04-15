using B2B.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2B.Infrastructure.OrderItems.Persistence;

public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(r => r.Id)
               .ValueGeneratedNever();

        builder.HasIndex(o => o.Quantity);
        builder.Property(o => o.UnitPrice);
        builder.Property(o => o.ProductId);       
        builder.Property(o => o.OrderId);       
    }
}