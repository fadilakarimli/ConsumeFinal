using FinalProjectConsume.Models.Slider;
using FinalProjectConsume.Models.SpecialOffer;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface ISpecialOfferService
    {
        Task<IEnumerable<SpecialOffer>> GetAllAsync();
        Task<HttpResponseMessage> DeleteAsync(int id);
        Task<HttpResponseMessage> CreateAsync(SpecialOfferCreate model);
        Task<HttpResponseMessage> EditAsync(int id, SpecialOfferEdit model);
        Task<SpecialOffer> GetByIdAsync(int id);
    }
}
