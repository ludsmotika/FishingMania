namespace FishingMania.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CatchesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Catches.Any())
            {
                return;
            }

            //await dbContext.Catches.AddAsync(new Catch() { ApplicationUserId = "d509c2c7-6949-451f-b05a-1053fb29859d", Description = "This one was very hard to catch. I have been pulling him for around 20 minutes. I caught it on my favourite fishing lure.", FishWeight = 1.35m, FishSpeciesId = 2, FishingSpotId = 1, ImageId = 18 });
            //await dbContext.Catches.AddAsync(new Catch() { ApplicationUserId = "d509c2c7-6949-451f-b05a-1053fb29859d", Description = "This is the first time I ever caught a fish from this species. I am very happy.", FishWeight = 3.35m, FishSpeciesId = 10, FishingSpotId = 2, ImageId = 19 });

            await dbContext.SaveChangesAsync();
        }
    }
}
