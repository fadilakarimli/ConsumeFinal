using FinalProjectConsume.Models.Paginate;
using FinalProjectConsume.Models.TeamMember;
using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Models.TrandingDestination;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface ITrandingDestinationService
    {
        Task<IEnumerable<TrandingDestination>> GetAllAsync();
        Task<TrandingDestination> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(TrandingDestinationCreate model);
        Task<HttpResponseMessage> EditAsync(int id, TrandingDestinationEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
        Task<Paginated<Tour>> GetPaginatedAsync(int page = 1, int take = 5);
    }
}
