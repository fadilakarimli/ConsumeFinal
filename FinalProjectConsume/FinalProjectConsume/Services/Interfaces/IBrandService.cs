using FinalProjectConsume.Models.Brand;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<HttpResponseMessage> DeleteAsync(int id);
        Task<HttpResponseMessage> CreateAsync(BrandCreate model);
        Task<HttpResponseMessage> EditAsync(int id, BrandEdit model);
        Task<Brand> GetByIdAsync(int id);


    }
}
