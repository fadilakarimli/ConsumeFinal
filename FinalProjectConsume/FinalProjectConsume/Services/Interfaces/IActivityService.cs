using FinalProjectConsume.Models.Activity;
using FinalProjectConsume.Models.Blog;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<Activity>> GetAllAsync();
        Task<Activity> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(ActivityCreate model);
        Task<HttpResponseMessage> EditAsync(int id, ActivityEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
