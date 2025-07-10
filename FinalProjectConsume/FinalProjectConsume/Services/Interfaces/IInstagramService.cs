using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.Instagram;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IInstagramService
    {
        Task<IEnumerable<Instagram>> GetAllAsync();
        Task<HttpResponseMessage> DeleteAsync(int id);
        Task<HttpResponseMessage> CreateAsync(InstagramCreate model);
        Task<HttpResponseMessage> EditAsync(int id, InstagramEdit model);
        Task<Instagram> GetByIdAsync(int id);
    }
}
