namespace FishingMania.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;

    public class ProductsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Products.Any())
            {
                return;
            }


            Image daiwaReelFirst = new Image() { URL = "/img/products/daiwaReelFirst.jpg" };
            Image daiwaReelSecond = new Image() { URL = "/img/products/daiwaReelSecond.jpg" };
            Image shimanoRodFirst = new Image() { URL = "/img/products/shimanoRodFirst.jpg" };
            Image shimanoRodSecond = new Image() { URL = "/img/products/shimanoRodSecond.jpg" };
            Image shimanoRodThird = new Image() { URL = "/img/products/shimanoRodThird.jpg" };
            Image trabuccoLineFirst = new Image() { URL = "/img/products/trabuccoLineFirst.jpg" };
            Image trabuccoLineSecond = new Image() { URL = "/img/products/trabuccoLineSecond.jpg" };

            // Products
            await dbContext.Images.AddAsync(daiwaReelFirst);
            await dbContext.Images.AddAsync(daiwaReelSecond);
            await dbContext.Images.AddAsync(shimanoRodFirst);
            await dbContext.Images.AddAsync(shimanoRodSecond);
            await dbContext.Images.AddAsync(shimanoRodThird);
            await dbContext.Images.AddAsync(trabuccoLineFirst);
            await dbContext.Images.AddAsync(trabuccoLineSecond);

            List<Image> imagesForFirstProduct = new List<Image>() { daiwaReelFirst, daiwaReelSecond };

            await dbContext.Products.AddAsync(new Product()
            {
                Name = "Daiwa Infinity",
                Description = "A perfect daiwa reel so you can catch the biggest fish on the spot! It can fit up to 200 meters of 0.20 line.",
                Price = Math.Round(250.99m, 2),
                Images = imagesForFirstProduct,
                AmountInStock = 10,
                CategoryId = 2,
                ManufacturerId = 2,
            });

            List<Image> imagesForSecondProduct = new List<Image>() { shimanoRodFirst, shimanoRodSecond, shimanoRodThird };

            await dbContext.Products.AddAsync(new Product()
            {
                Name = "Shimano Blaster",
                Description = "A perfect shimano rod which will help you with pulling the fishes faster than ever! The length of the rod is 3.20 meters and it has two parts.",
                Price = Math.Round(180.99m, 2),
                Images = imagesForSecondProduct,
                AmountInStock = 40,
                CategoryId = 1,
                ManufacturerId = 1,
            });

            List<Image> imagesForThirdProduct = new List<Image>() { trabuccoLineFirst, trabuccoLineSecond };

            await dbContext.Products.AddAsync(new Product()
            {
                Name = "Trabucco StrongLine",
                Description = "The perfect line made by Trabucco for you to catch the best fishes! The length of the line is 150 meters and 0.22 size.",
                Price = Math.Round(15.99m, 2),
                Images = imagesForThirdProduct,
                AmountInStock = 20,
                CategoryId = 3,
                ManufacturerId = 4,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
