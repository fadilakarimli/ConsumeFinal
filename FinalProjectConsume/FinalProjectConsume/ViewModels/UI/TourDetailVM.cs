using FinalProjectConsume.Models.Experience;
using FinalProjectConsume.Models.Plan;
using FinalProjectConsume.Models.Tour;

namespace FinalProjectConsume.ViewModels.UI
{
    public class TourDetailVM
    {
        public Tour Tour { get; set; }
        public string SearchTerm { get; set; }

        public List<ReviewCreateVM> Reviews { get; set; } = new List<ReviewCreateVM>();
        public List<Plan> Plans { get; set; }
        public List<Experience> Experiences { get; set; }

        public double AverageStar => Reviews != null && Reviews.Any()
            ? Math.Round(Reviews.Average(x => x.Star), 1)
            : 0.0;
        public int ReviewCount => Reviews?.Count ?? 0;
        public Dictionary<int, int> StarCounts => Reviews?.GroupBy(r => r.Star).ToDictionary(g => g.Key, g => g.Count())?? new Dictionary<int, int>();
        public List<SettingVM> Settings { get; set; }





    }
}
