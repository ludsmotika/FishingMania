using FishingMania.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FishingMania.Data.Seeding
{
    public class ManufacturersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Manufacturers.Any())
            {
                return;
            }

            await dbContext.Manufacturers.AddAsync(new Manufacturer { Name = "Shimano", Country = "Japan" });
            await dbContext.Manufacturers.AddAsync(new Manufacturer { Name = "Daiwa", Country = "Japan" });
            await dbContext.Manufacturers.AddAsync(new Manufacturer { Name = "FenWick", Country = "Scotland" });
            await dbContext.Manufacturers.AddAsync(new Manufacturer { Name = "Trabucco", Country = "Italia" });
            await dbContext.Manufacturers.AddAsync(new Manufacturer { Name = "FilStar", Country = "Bulgaria" });
            await dbContext.Manufacturers.AddAsync(new Manufacturer { Name = "Rapala", Country = "Finland" });

            await dbContext.SaveChangesAsync();
        }
    }
}
