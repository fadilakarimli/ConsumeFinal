using FinalProjectConsume.Models.AboutAgency;
using FinalProjectConsume.Models.Activity;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IAboutAgencyService
    {
        Task<IEnumerable<AboutAgency>> GetAllAsync();
        Task<AboutAgency> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(AboutAgencyCreate model);
        Task<HttpResponseMessage> EditAsync(int id, AboutAgencyEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
