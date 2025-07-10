using FinalProjectConsume.Models.Blog;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(BlogCreate model);
        Task<HttpResponseMessage> EditAsync(int id, BlogEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
