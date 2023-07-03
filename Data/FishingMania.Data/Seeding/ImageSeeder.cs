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
            await dbContext.Images.AddAsync(new Image() { URL = "https://thumbs.dreamstime.com/b/northern-pike-isolated-over-white-background-50482311.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ1ZZfRyEyd8sQWcJBH93eNE2-YiC73xulGlpfEJrhtigJIHPEYJDLaxotctO6Mw6-TLNI&usqp=CAU" });
            await dbContext.Images.AddAsync(new Image() { URL = "https://storage.googleapis.com/bol-cdn/2020/02/bass-largemouth.png" });
            await dbContext.Images.AddAsync(new Image() { URL = "https://upload.wikimedia.org/wikipedia/commons/9/94/Ameiurus_melas_by_Duane_Raver.png" });
            await dbContext.Images.AddAsync(new Image() { URL = "https://www.shutterstock.com/image-photo/freshwater-fish-isolated-on-white-260nw-1820924963.jpg" });

            // Fishing spots photos
            await dbContext.Images.AddAsync(new Image() { URL = "https://img.cms.bweb.bg/media/images/640x360/Nov2020/2112311280.webp" });
            await dbContext.Images.AddAsync(new Image() { URL = "https://fishi.bg/wp-content/uploads/2023/05/yazovir-pancharevo.jpg" });

            // Catches
            await dbContext.Images.AddAsync(new Image() { URL = "https://cdn.mos.cms.futurecdn.net/xUaCjunfKid4mEniMtZNBj.jpg" });
            await dbContext.Images.AddAsync(new Image() { URL = "https://www.westin-fishing.com/media/inrb5ffm/2.jpg?width=933&upscale=false&bgcolor=white" });

            await dbContext.SaveChangesAsync();
        }
    }
}
