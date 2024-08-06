using Microsoft.EntityFrameworkCore;
using MyBlog.API.Models.Domain;

namespace MyBlog.API.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> blogPosts { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<BlogImages> BlogImages { get; set; }
    }

}
