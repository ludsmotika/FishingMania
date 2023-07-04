namespace FishingMania.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;

    public class FishingSpotsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.FishingSpots.Any())
            {
                return;
            }

            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Drenov Dol", Description = "Drenov Dol is a reservoir in Western Bulgaria. A perfect place to enjoy fishing and many other outdoor activities. The reservoir has great fish wealth. In addition, there is an association that monitors proper fishing and often cleans and stocks a lot of fish.", Latitude = 42.3024m, Longitude = 22.6919m, ImageId = 6, FishingSpotType = FishingSpotType.Reservoir });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Lake Pancharevo", Description = "Lake Pancharevo is a lake in the upper course of the Iskar River, on the territory of the municipality of Sofia. The beautiful nature and its proximity to Sofia make it a favorite place for outings and rest for Sofia residents. In addition to the possibility of fishing, there are also water bikes that you can ride.", Latitude = 42.5908m, Longitude = 23.4194m, ImageId = 7, FishingSpotType = FishingSpotType.Lake });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Lake Pancharevo", Description = "Lake Pancharevo is a lake in the upper course of the Iskar River, on the territory of the municipality of Sofia. The beautiful nature and its proximity to Sofia make it a favorite place for outings and rest for Sofia residents. In addition to the possibility of fishing, there are also water bikes that you can ride.", Latitude = 42.5908m, Longitude = 23.4194m, ImageId = 8, FishingSpotType = FishingSpotType.Swamp });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Lake Pancharevo", Description = "Lake Pancharevo is a lake in the upper course of the Iskar River, on the territory of the municipality of Sofia. The beautiful nature and its proximity to Sofia make it a favorite place for outings and rest for Sofia residents. In addition to the possibility of fishing, there are also water bikes that you can ride.", Latitude = 42.5908m, Longitude = 23.4194m, ImageId = 9, FishingSpotType = FishingSpotType.River });

            await dbContext.SaveChangesAsync();
        }
    }
}
