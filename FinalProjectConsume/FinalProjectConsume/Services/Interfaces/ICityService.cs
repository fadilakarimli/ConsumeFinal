using FinalProjectConsume.Models.City;
using FinalProjectConsume.Models.Country;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllAsync();
        Task<City> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(CityCreate model);
        Task<HttpResponseMessage> EditAsync(int id, CityEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
