namespace FinalProjectConsume.Models.AboutApp
{
    public class AboutAppEdit
    {
        public IFormFile? Image { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Text { get; set; }
        public IFormFile? AppleImage { get; set; }
        public IFormFile ?PlayStoreImage { get; set; }
        public IFormFile? BackgroundImage { get; set; }
    }
}
