namespace FishingMania.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class FishSpeciesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.FishSpecies.Any())
            {
                return;
            }

            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Pike", Type = FishType.Carnivorous, ImageId = 1 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Carp", Type = FishType.Omnivorous, ImageId = 2 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Bass", Type = FishType.Carnivorous, ImageId = 3 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Catfish", Type = FishType.Omnivorous, ImageId = 4 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Silver Carp", Type = FishType.Herbivorous, ImageId = 5 });

            await dbContext.SaveChangesAsync();
        }
    }
}
