using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.API.Models.Domain;
using MyBlog.API.Models.DTO;
using MyBlog.API.Repositories.Interface;

namespace MyBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        public ImagesController(IImageRepository imageRepository)
        {
            ImageRepository = imageRepository;
        }

        public IImageRepository ImageRepository { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllImage()
        {
            var images = await ImageRepository.GetAllImages();
            var response = new List<BlogImageResponseDTO>();

            foreach (var image in images)
            {
                response.Add(new BlogImageResponseDTO
                {
                    id = image.id,
                    DateCreated = image.DateCreated,
                    FileExtension = image.FileExtension,
                    Title = image.Title,
                    FileName = image.FileName,
                    Url = image.Url
                });
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImages([FromForm] IFormFile file, [FromForm] string fileName,
            [FromForm] string title)
        {
            if(ModelState.IsValid)
            {
                var imageRequest = new BlogImages
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    Title = title,
                    FileName = fileName,
                    DateCreated = DateTime.Now
                };

                imageRequest = await ImageRepository.UploadImage(file, imageRequest);

                var response = new BlogImageResponseDTO
                {
                    id = imageRequest.id,
                    DateCreated = imageRequest.DateCreated,
                    FileExtension = imageRequest.FileExtension,
                    Title = imageRequest.Title,
                    FileName = imageRequest.FileName,
                    Url = imageRequest.Url
                };

                return Ok(response);
            }

            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtenstions = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowedExtenstions.Contains(Path.GetExtension(file.FileName)))
            {
                ModelState.AddModelError("file", "Invalid Image Format");
            }
            if(file.Length > 10485760)
            {
                ModelState.AddModelError("file", "Length of File must be less than 10MB");
            }
        }
    }
}
