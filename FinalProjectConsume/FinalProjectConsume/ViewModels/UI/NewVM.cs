using FinalProjectConsume.Models.Blog;

namespace FinalProjectConsume.ViewModels.UI
{
    public class NewVM
    {
        public List<Blog> Blogs { get; set; }
        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<SettingVM> Settings { get; set; }


    }
}
