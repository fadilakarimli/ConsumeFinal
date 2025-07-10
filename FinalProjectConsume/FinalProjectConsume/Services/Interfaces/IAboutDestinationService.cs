using FinalProjectConsume.Models.AboutAgency;
using FinalProjectConsume.Models.AboutDestination;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IAboutDestinationService
    {
        Task<IEnumerable<AboutDestination>> GetAllAsync();
        Task<AboutDestination> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(AboutDestinationCreate model);
        Task<HttpResponseMessage> EditAsync(int id, AboutDestinationEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
