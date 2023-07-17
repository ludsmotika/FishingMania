namespace FishingMania.Services.Data.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class ImageService : IImageService
    {
        private readonly IDeletableEntityRepository<Image> imagesRepository;
        private readonly ICloudinaryService cloudinaryService;

        public ImageService(IDeletableEntityRepository<Image> imagesRepository, ICloudinaryService cloudinaryService)
        {
            this.imagesRepository = imagesRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<Image> AddByFile(IFormFile imageFile, string name)
        {
            try
            {
                // Save image to Cloudinary
                var imageUrl = await this.cloudinaryService
                    .UploadAsync(imageFile, name);

                // Save image to database
                var existingImage = await this.imagesRepository.All().Where(i => i.URL == imageUrl).FirstOrDefaultAsync();
                if (existingImage != null)
                {
                    return existingImage;
                }

                var image = new Image() { URL = imageUrl };
                await this.imagesRepository.AddAsync(image);
                await this.imagesRepository.SaveChangesAsync();
                return await this.GetByUrlAsync(imageUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Image> GetImageByIdAsync(int id)
        {
            return this.imagesRepository.All().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        private async Task<Image> GetByUrlAsync(string url)
        {
            return await this.imagesRepository.All().Where(x => x.URL == url).FirstOrDefaultAsync();
        }
    }
}
