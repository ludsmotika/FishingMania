namespace FishingMania.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;

    internal class ImageSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Images.Any())
            {
                return;
            }

            // FishSpecies photos
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/bass.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/carp.png" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/catfish.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/chub.png" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/commonBleak.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/commonNase.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/crusianCarp.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/garfish.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/perch.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/pike.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/rudd.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/silverCarp.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/trout.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/whiteBarbel.png" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishSpecies/whiteCarp.png" });

            // Fishing spots photos
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/akrutino.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/alepu.jpeg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/batak.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/dospad.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/drenovDol.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/dunav.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/iskar.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/kamchiq.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/kleptuza.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/ledenoto.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/marica.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/orlovo.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/pancharevo.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/popovo.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/ropotamo.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/sreburna.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/fishingSpots/struma.jpg" });

            // Catches
            await dbContext.Images.AddAsync(new Image() { URL = "/img/catches/carpCatch.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "/img/catches/pikeCatch.jpg" });

            await dbContext.SaveChangesAsync();
        }
    }
}
