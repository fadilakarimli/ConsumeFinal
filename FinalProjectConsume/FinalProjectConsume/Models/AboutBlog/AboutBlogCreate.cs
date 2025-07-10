namespace FinalProjectConsume.Models.AboutBlog
{
    public class AboutBlogCreate
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public IFormFile Image { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
