using FinalProjectConsume.Models.AboutAgency;
using FinalProjectConsume.Models.AboutApp;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IAboutAppService
    {
        Task<IEnumerable<AboutApp>> GetAllAsync();
        Task<AboutApp> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(AboutAppCreate model);
        Task<HttpResponseMessage> EditAsync(int id, AboutAppEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
