using Microsoft.EntityFrameworkCore;
using MyBlog.API.Data;
using MyBlog.API.Models.Domain;
using MyBlog.API.Repositories.Interface;

namespace MyBlog.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        public ApplicationDBContext DBContext { get; }

        public BlogPostRepository(ApplicationDBContext dBContext)
        {
            DBContext = dBContext;
        }
        public async Task<BlogPost> createBlogpost(BlogPost blogPost)
        {
            await DBContext.blogPosts.AddAsync(blogPost);
            await DBContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> getAllBlogPost()
        {
            return await DBContext.blogPosts.Include(x => x.categories).ToListAsync();
        }

        public async Task<BlogPost> getBlogPostbyID(Guid id)
        {
            return await DBContext.blogPosts.Include(x => x.categories).FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<BlogPost?> updateBlogPost(BlogPost blogPost)
        {
            var existingBlogPost = await DBContext.blogPosts.Include(x => x.categories).FirstOrDefaultAsync(x => x.id == blogPost.id);  
            if (existingBlogPost == null)
            {
                return null;
            }

            DBContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);
            existingBlogPost.categories = blogPost.categories;
            await DBContext.SaveChangesAsync();

            return blogPost;

        }

        public async Task<BlogPost> DeleteBlogpostByID(Guid id)
        {
            var existingBlogPost = await DBContext.blogPosts.FirstOrDefaultAsync(x => x.id == id);
            if(existingBlogPost == null)
            {
                return null;
            }

            DBContext.blogPosts.Remove(existingBlogPost);
            await DBContext.SaveChangesAsync();
            return existingBlogPost;
        }

        public async Task<BlogPost?> getBlogPostbyurlhandle(string urlhandle)
        {
            return await DBContext.blogPosts.Include(x => x.categories).FirstOrDefaultAsync(x => x.url_handle == urlhandle);
        }
    }
}
