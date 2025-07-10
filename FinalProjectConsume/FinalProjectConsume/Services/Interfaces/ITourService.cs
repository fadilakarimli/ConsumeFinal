using FinalProjectConsume.Models.Paginate;
using FinalProjectConsume.Models.Tour;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface ITourService
    {
        Task<IEnumerable<Tour>> GetAllAsync();
        Task<Tour> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(TourCreate model);
        Task<HttpResponseMessage> EditAsync(int id, TourEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
        Task<IEnumerable<Tour>> SearchAsync(TourSearchRequest searchRequest);
        Task<IEnumerable<Tour>> FilterAsync(TourFilter filter);
        Task<Paginated<Tour>> GetPaginatedAsync(int page = 1, int take = 5);
    }
}
