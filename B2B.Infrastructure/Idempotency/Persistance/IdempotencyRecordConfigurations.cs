using B2B.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2B.Infrastructure.Idempotency.Persistance;

public class IdempotencyRecordConfigurations : IEntityTypeConfiguration<IdempotencyRecord>
{
    public void Configure(EntityTypeBuilder<IdempotencyRecord> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(r => r.Id)
               .ValueGeneratedNever();

        builder.HasIndex(c => new { c.Key, c.UserId })
            .IsUnique();

        builder.HasIndex(o => o.UserId);
        builder.Property(o => o.ResultId);
        builder.Property(o => o.CreatedAt);
    }
}