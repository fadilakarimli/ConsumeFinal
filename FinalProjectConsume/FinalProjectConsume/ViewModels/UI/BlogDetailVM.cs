using FinalProjectConsume.Models.Blog;

namespace FinalProjectConsume.ViewModels.UI
{
    public class BlogDetailVM
    {
        public Blog Blog { get; set; } // Əsas blog (detal üçün)
        public List<Blog> RelatedBlogs { get; set; }
        public List<SettingVM> Settings { get; set; }

    }
}
    