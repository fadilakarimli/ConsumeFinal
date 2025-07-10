using FinalProjectConsume.ViewModels.UI;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface ISettingService
    {
        Task<IEnumerable<SettingVM>> GetAllAsync();
        Task<SettingVM> GetByIdAsync(int id);
        Task<ResponseVM> EditAsync(int id, SettingVM setting);
    }
}
