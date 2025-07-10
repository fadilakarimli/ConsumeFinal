using FinalProjectConsume.Models.AboutApp;
using FinalProjectConsume.Models.AboutTravil;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IAboutTravilService
    {
        Task<IEnumerable<AboutTravil>> GetAllAsync();
        Task<AboutTravil> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(AboutTravilCreate model);
        Task<HttpResponseMessage> EditAsync(int id, AboutTravilEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
