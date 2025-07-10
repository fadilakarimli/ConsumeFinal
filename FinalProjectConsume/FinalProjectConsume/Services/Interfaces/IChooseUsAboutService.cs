using FinalProjectConsume.Models.Blog;
using FinalProjectConsume.Models.ChooseUsAbout;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IChooseUsAboutService
    {
        Task<IEnumerable<ChooseUsAbout>> GetAllAsync();
        Task<ChooseUsAbout> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(ChooseUsAboutCreate model);
        Task<HttpResponseMessage> EditAsync(int id, ChooseUsAboutEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
