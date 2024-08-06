using MyBlog.API.Models.Domain;

namespace MyBlog.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> createBlogpost(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> getAllBlogPost();
        Task<BlogPost?> getBlogPostbyID(Guid id);
        Task<BlogPost?> getBlogPostbyurlhandle(string urlhandle);
        Task<BlogPost?> updateBlogPost(BlogPost blogPost); 
        Task<BlogPost?> DeleteBlogpostByID(Guid id);
    }
}
