using FinalProjectConsume.Models.DestinationFeature;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IDestinationFeatureService
    {
        Task<IEnumerable<DestinationFeature>> GetAllAsync();
        Task<DestinationFeature> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(DestinationFeatureCreate model);
        Task<HttpResponseMessage> EditAsync(int id, DestinationFeatureEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
