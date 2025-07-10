using FinalProjectConsume.Models.Blog;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface INewService
    {
        Task<IEnumerable<Blog>> SearchBlogsAsync(string query);

    }
}
