using FinalProjectConsume.Models.Activity;
using FinalProjectConsume.Models.SliderInfo;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface ISliderInfoService
    {
        Task<IEnumerable<SliderInfo>> GetAllAsync();
        Task<SliderInfo> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(SliderInfoCreate model);
        Task<HttpResponseMessage> EditAsync(int id, SliderInfoEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
