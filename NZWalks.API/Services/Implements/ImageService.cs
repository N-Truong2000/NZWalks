using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Services.Implements
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _http;
        private readonly NZWalksDbContext _dbContext;

        public ImageService(IWebHostEnvironment environment, IHttpContextAccessor http, NZWalksDbContext dbContext)
        {
            _environment = environment;
            _http = http;
            _dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var loaclFilePath = Path.Combine(_environment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            using (var stream = new FileStream(loaclFilePath, FileMode.Create))
            {
                await image.File.CopyToAsync(stream);
                //
                var urlFilePath = $"{_http.HttpContext.Request.Scheme}://{_http.HttpContext.Request.Host}{_http.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
                image.FilePath = urlFilePath;
                await image.File.CopyToAsync(stream);
                await _dbContext.AddAsync(image);
                await _dbContext.SaveChangesAsync();
            }
            return image;
        }
    }
}
