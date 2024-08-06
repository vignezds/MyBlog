namespace MyBlog.API.Models.Domain
{
    public class BlogPost
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string short_description { get; set; }
        public string content { get; set; }
        public string featured_image_url { get; set; }
        public string url_handle { get; set; }
        public string author { get; set; }
        public DateTime published_date { get; set; }
        public bool is_visible { get; set; }
        public ICollection<Category> categories { get; set; }
    }
}
