using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Blog
{
    public class BlogEdit
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string? Content { get; set; }
        //public string Author { get; set; }
        //public int CommentCount { get; set; }
        public IFormFile ?Image { get; set; }
    }
}
