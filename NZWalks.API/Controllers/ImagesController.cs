using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Services;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
           _imageService = imageService;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto file)
        {
            ValidateFileUpload(file);
            if (ModelState.IsValid)
            {
                try
                {
                    var imageDomailModel = new Image
                    {
                        File = file.File,
                        FileExtension = Path.GetExtension(file.File.FileName),
                        FileSizeInBytes=file.File.Length,
                        FileName=file.FileName,
                        FileDescription = file.FileDescription,
                    };
                    await _imageService.Upload(imageDomailModel);
                    return Ok("File uploaded successfully.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the file: " + ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDto file)
        {
            var allowerExtensions = new string[] { ".jpg", ".jpeg", ",png" };
            var fileExtension = Path.GetExtension(file.File.FileName).ToLower();
            var maximumFileSizeInBytes = 10485760;
            if (!allowerExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("file", "Unsupported file extension.");
            }
            if (file.File.Length > maximumFileSizeInBytes)
            {
                ModelState.AddModelError("file", "File size exceeds the maximum allowed.(10MB)");
            }
            if (file.File.Length == 0)
            {
                ModelState.AddModelError("file", "File is empty.");
            }
        }
    }
}
