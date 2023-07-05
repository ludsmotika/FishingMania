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

            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Bass", Type = FishType.Carnivorous, ImageId = 1 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Carp", Type = FishType.Omnivorous, ImageId = 2 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Catfish", Type = FishType.Omnivorous, ImageId = 3 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Chub", Type = FishType.Omnivorous, ImageId = 4 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Common Bleak", Type = FishType.Herbivorous, ImageId = 5 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Common Nase", Type = FishType.Herbivorous, ImageId = 6 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Crusian Carp", Type = FishType.Omnivorous, ImageId = 7 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Garfish", Type = FishType.Herbivorous, ImageId = 8 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Perch", Type = FishType.Carnivorous, ImageId = 9 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Pike", Type = FishType.Carnivorous, ImageId = 10 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Rudd", Type = FishType.Herbivorous, ImageId = 11 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Silver Carp", Type = FishType.Herbivorous, ImageId = 12 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "Trout", Type = FishType.Carnivorous, ImageId = 13 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "White Barbel", Type = FishType.Herbivorous, ImageId = 14 });
            await dbContext.FishSpecies.AddAsync(new FishSpecies { Name = "White Carp", Type = FishType.Omnivorous, ImageId = 15 });

            await dbContext.SaveChangesAsync();
        }
    }
}
