namespace FinalProjectConsume.Models.AboutBlog
{
    public class AboutBlog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
