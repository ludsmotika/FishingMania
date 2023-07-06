namespace FishingMania.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;

    public class FishSpeciesFishingSpotsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.FishSpeciesFishingSpots.Any())
            {
                return;
            }

            int seed = 123;
            Random random = new Random(seed);

            List<Tuple<int, int>> alreadyAddedSpeciesToSpot = new List<Tuple<int, int>>();
            for (int i = 1; i <= 17; i++)
            {
                for (int k = 1; k < 5; k++)
                {
                    int randomSpeciesId = random.Next(1, 16);
                    if (!alreadyAddedSpeciesToSpot.Contains(Tuple.Create(i,randomSpeciesId)))
                    {
                        await dbContext.FishSpeciesFishingSpots.AddAsync(new FishSpeciesFishingSpots() { FishingSpotId = i, FishSpeciesId = randomSpeciesId });
                    }

                    alreadyAddedSpeciesToSpot.Add(Tuple.Create(i, randomSpeciesId));
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
