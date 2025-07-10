namespace FinalProjectConsume.Models.AboutTravil
{
    public class AboutTravilEdit
    {
        public IFormFile? Image { get; set; }
        public IFormFile? SmallImage { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Subtitle { get; set; }
        public string SubDesc { get; set; }
    }
}
