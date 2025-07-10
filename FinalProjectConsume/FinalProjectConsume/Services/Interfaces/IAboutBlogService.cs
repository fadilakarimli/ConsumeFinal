using FinalProjectConsume.Models.AboutBlog;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IAboutBlogService
    {
        Task<IEnumerable<AboutBlog>> GetAllAsync();
        Task<AboutBlog> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(AboutBlogCreate model);
        Task<HttpResponseMessage> EditAsync(int id, AboutBlogEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
