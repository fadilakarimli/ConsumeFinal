using FinalProjectConsume.ViewModels.UI;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewVM>> GetAllAsync();
    }
}
