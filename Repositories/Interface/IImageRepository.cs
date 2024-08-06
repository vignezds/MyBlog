using MyBlog.API.Models.Domain;

namespace MyBlog.API.Repositories.Interface
{
    public interface IImageRepository
    {
       Task<BlogImages> UploadImage(IFormFile file, BlogImages blogImages);

        Task<IEnumerable<BlogImages>> GetAllImages();
    }
}
