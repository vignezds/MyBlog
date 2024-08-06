namespace MyBlog.API.Models.DTO
{
    public class updateBlogPostdto
    {
        public string title { get; set; }
        public string short_description { get; set; }
        public string content { get; set; }
        public string featured_image_url { get; set; }
        public string url_handle { get; set; }
        public string author { get; set; }
        public DateTime published_date { get; set; }
        public bool is_visible { get; set; }
        public List<Guid> categories { get; set; } = new List<Guid>();
    }
}
