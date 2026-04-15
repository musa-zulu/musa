using B2B.Domain.Entities;
using B2B.Infrastructure.Common.Persistence;

namespace B2B.Infrastructure.Seeding;

public static class DbSeeder
{
    public static async Task Seed(AppDbContext dbContext)
    {
        if (dbContext.Products.Any()) return;

        dbContext.Products.AddRange(
            new Product
            {
                Name = "Laptop",
                SKU = "LAP-001",
                UnitPrice = 1500,
                AvailableQuantity = 10,
                Category = "electronics"
            },
            new Product
            {
                Name = "Phone",
                SKU = "PHN-001",
                UnitPrice = 1000,
                AvailableQuantity = 20,
                Category = "electronics"
            },
            new Product
            {
                Name = "Desk Chair",
                SKU = "CHR-001",
                UnitPrice = 1200,
                AvailableQuantity = 15,
                Category = "furniture"
            });

        await dbContext.SaveChangesAsync();
    }
}
