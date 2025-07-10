using FinalProjectConsume.Models.Activity;
using FinalProjectConsume.Models.Country;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllAsync();
        Task<Country> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(CountryCreate model);
        Task<HttpResponseMessage> EditAsync(int id, CountryEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
