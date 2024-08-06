namespace MyBlog.API.Models.Domain
{
    public class Category
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string url_handle { get; set; }
        public ICollection<BlogPost> blog_posts { get; set; }
    }
}
