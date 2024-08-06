using Microsoft.EntityFrameworkCore;
using MyBlog.API.Data;
using MyBlog.API.Models.Domain;
using MyBlog.API.Repositories.Interface;

namespace MyBlog.API.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        public ImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDBContext applicationDBContext)
        {
            WebHostEnvironment = webHostEnvironment;
            HttpContextAccessor = httpContextAccessor;
            ApplicationDBContext = applicationDBContext;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }
        public ApplicationDBContext ApplicationDBContext { get; }

        public async Task<IEnumerable<BlogImages>> GetAllImages()
        {
            return await ApplicationDBContext.BlogImages.ToListAsync();   
        }

        public async Task<BlogImages> UploadImage(IFormFile file, BlogImages blogImages)
        {
            //STEP 1 : upload to local folder
            var localpath = Path.Combine(WebHostEnvironment.ContentRootPath, "Images", $"{blogImages.FileName}{blogImages.FileExtension}");
            var stream = new FileStream(localpath, FileMode.Create);
            await file.CopyToAsync(stream);
            //STEP2 : upload it to DB

            var httpRequest = HttpContextAccessor.HttpContext.Request;
            string Url = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImages.FileName}{blogImages.FileExtension}"; 
            blogImages.Url = Url;

            await ApplicationDBContext.BlogImages.AddAsync(blogImages);
            await ApplicationDBContext.SaveChangesAsync();

            return blogImages;

        }
    }
}
