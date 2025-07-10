using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.Slider;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<Slider>> GetAllAsync();
        Task<HttpResponseMessage> DeleteAsync(int id);
        Task<HttpResponseMessage> CreateAsync(SliderCreate model);
        Task<HttpResponseMessage> EditAsync(int id, SliderEdit model);
        Task<Slider> GetByIdAsync(int id);
    }
}
