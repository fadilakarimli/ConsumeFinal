using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Blog
{
    public class BlogCreate
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string? Content { get; set; }
        //public string Author { get; set; }
        //public int CommentCount { get; set; }
        [Required(ErrorMessage = "Please select an image")]
        public IFormFile Image { get; set; }
    }
}
