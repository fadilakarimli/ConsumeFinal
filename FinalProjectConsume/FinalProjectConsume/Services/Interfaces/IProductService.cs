using FinalProjectConsume.Models.Product;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(ProductCreate model);
        Task<HttpResponseMessage> EditAsync(int id, ProductEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
