using FinalProjectConsume.Models.Paginate;
using FinalProjectConsume.Models.Tour;

namespace FinalProjectConsume.ViewModels.UI
{
    public class TourIndexVM
    {
        public Paginated<Tour> PaginatedTours { get; set; }
        public RolePermissionVM Permissions { get; set; }


        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

    }
}
