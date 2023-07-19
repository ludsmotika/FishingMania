namespace FishingMania.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;

    public class ProductCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ProductCategories.Any())
            {
                return;
            }

            await dbContext.ProductCategories.AddAsync(new ProductCategory { Name = "Rods" });
            await dbContext.ProductCategories.AddAsync(new ProductCategory { Name = "Reels" });
            await dbContext.ProductCategories.AddAsync(new ProductCategory { Name = "Lines" });
            await dbContext.ProductCategories.AddAsync(new ProductCategory { Name = "Lures" });
            await dbContext.ProductCategories.AddAsync(new ProductCategory { Name = "Accessories" });

            await dbContext.SaveChangesAsync();
        }
    }
}
