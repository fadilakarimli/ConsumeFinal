using FinalProjectConsume.Models.Country;
using FinalProjectConsume.Models.Experience;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IExperienceService
    {
        Task<IEnumerable<Experience>> GetAllAsync();
        Task<Experience> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(ExperienceCreate model);
        Task<HttpResponseMessage> EditAsync(int id, ExperienceEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
        Task<IEnumerable<Experience>> GetByTourIdAsync(int tourId);
    }
}
