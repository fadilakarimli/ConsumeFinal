using FinalProjectConsume.Models.AboutApp;
using FinalProjectConsume.Models.Plan;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IPlanService
    {
        Task<IEnumerable<Plan>> GetAllAsync();
        Task<Plan> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(PlanCreate model);
        Task<HttpResponseMessage> EditAsync(int id, PlanEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);

    }
}
