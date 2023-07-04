namespace FishingMania.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using FishingMania.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        Task<Image> AddByFile(IFormFile imageFile, string name);

        Task<Image> GetImageByIdAsync(int id);
    }
}
