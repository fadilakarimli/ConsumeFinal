using FinalProjectConsume.Models.Activity;
using FinalProjectConsume.Models.Amenity;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IAmenityService
    {
        Task<IEnumerable<Amenity>> GetAllAsync();
        Task<Amenity> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(AmenityCreate model);
        Task<HttpResponseMessage> EditAsync(int id, AmenityEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
