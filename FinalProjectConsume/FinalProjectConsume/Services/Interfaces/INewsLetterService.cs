using FinalProjectConsume.Models.NewsLetter;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface INewsLetterService
    {
        Task<IEnumerable<NewsLetter>> GetAllAsync();
        Task<HttpResponseMessage> CreateAsync(NewsLetterCreate model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
