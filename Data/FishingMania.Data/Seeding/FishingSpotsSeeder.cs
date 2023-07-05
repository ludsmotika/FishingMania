﻿namespace FishingMania.Data.Seeding
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

            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Akrutino", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 42.330530m, Longitude = 27.725148m, ImageId = 16, FishingSpotType = FishingSpotType.Swamp });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Alepu", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 42.364081m, Longitude = 27.706312m, ImageId = 17, FishingSpotType = FishingSpotType.Swamp });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Batak", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 41.971169m, Longitude = 24.188204m, ImageId = 18, FishingSpotType = FishingSpotType.Reservoir });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Dospad", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 41.662060m, Longitude = 24.144449m, ImageId = 19, FishingSpotType = FishingSpotType.Reservoir });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Drenov dol", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 42.301685m, Longitude = 22.689381m, ImageId = 20, FishingSpotType = FishingSpotType.Reservoir });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Dunav", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 43.827158m, Longitude = 25.917761m, ImageId = 21, FishingSpotType = FishingSpotType.River });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Iskar", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 43.168616m, Longitude = 23.899523m, ImageId = 22, FishingSpotType = FishingSpotType.River });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Kamchiq", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 42.870142m, Longitude = 26.908328m, ImageId = 23, FishingSpotType = FishingSpotType.Reservoir });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Kleptuza", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 42.000495m, Longitude = 23.982034m, ImageId = 24, FishingSpotType = FishingSpotType.Lake });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Ice lake", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 42.182357m, Longitude = 23.589309m, ImageId = 25, FishingSpotType = FishingSpotType.Lake });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Marica", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 42.116004m, Longitude = 25.204764m, ImageId = 26, FishingSpotType = FishingSpotType.River });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Orlovo", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 43.687412m, Longitude = 28.558314m, ImageId = 27, FishingSpotType = FishingSpotType.Swamp });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Pancharevo", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 42.594479m, Longitude = 23.418329m, ImageId = 28, FishingSpotType = FishingSpotType.Reservoir });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Popovo", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 41.707006m, Longitude = 23.507487m, ImageId = 29, FishingSpotType = FishingSpotType.Lake });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Ropotamo", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 42.323454m, Longitude = 27.738802m, ImageId = 30, FishingSpotType = FishingSpotType.River });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Sreburna", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 44.111427m, Longitude = 27.061086m, ImageId = 31, FishingSpotType = FishingSpotType.Lake });
            await dbContext.FishingSpots.AddAsync(new FishingSpot() { Name = "Struma", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", Latitude = 42.256814m, Longitude = 22.851597m, ImageId = 32, FishingSpotType = FishingSpotType.River });

            await dbContext.SaveChangesAsync();
        }
    }
}
