namespace FishingMania.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;

    public class CatchesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Catches.Any())
            {
                return;
            }

            await dbContext.Catches.AddAsync(new Catch() { ApplicationUserId = "69206d10-c007-4981-8a3a-dc1567e058a6", Description = "This one was very hard to catch. I have been pulling him for around 20 minutes. I caught it on my favourite fishing lure.", FishWeight = 1.35m, FishSpeciesId = 2, FishingSpotId = 1, ImageId = 8 });
            await dbContext.Catches.AddAsync(new Catch() { ApplicationUserId = "69206d10-c007-4981-8a3a-dc1567e058a6", Description = "This is the first time I ever caught a fish from this species. I am very happy.", FishWeight = 3.35m, FishSpeciesId = 1, FishingSpotId = 2, ImageId = 9 });

            await dbContext.SaveChangesAsync();
        }
    }
}
