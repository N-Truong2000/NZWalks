using NZWalks.API.Models.Domain;

namespace NZWalks.API.Services
{
    public interface IImageService
    {
        Task<Image> Upload(Image image);
    }
}
