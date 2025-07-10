using FinalProjectConsume.Models.AboutAgency;
using FinalProjectConsume.Models.Activity;
using FinalProjectConsume.Models.Blog;
using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.City;
using FinalProjectConsume.Models.DestinationFeature;
using FinalProjectConsume.Models.Instagram;
using FinalProjectConsume.Models.NewsLetter;
using FinalProjectConsume.Models.Slider;
using FinalProjectConsume.Models.SliderInfo;
using FinalProjectConsume.Models.SpecialOffer;
using FinalProjectConsume.Models.TeamMember;
using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Models.TrandingDestination;

namespace FinalProjectConsume.ViewModels.UI
{
    public class HomePageVM
    {
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Instagram> Instagrams { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<DestinationFeature> DestinationFeatures { get; set; }
        public IEnumerable<TeamMember> TeamMembers { get; set; }
        public IEnumerable<TrandingDestination> TrandingDestinations { get; set; }
        public IEnumerable<Tour> Tours { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<NewsLetter> NewsLetters { get; set; }
        public IEnumerable<SliderInfo> SliderInfos { get; set; }
        public IEnumerable<SpecialOffer> SpecialOffers { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<AboutAgency> AboutAgencies { get; set; }
        public IEnumerable<Activity> Activities { get; set; }
        public string SearchTerm { get; set; }
        public List<ReviewVM> Reviews { get; set; } = new List<ReviewVM>();

    }
}
