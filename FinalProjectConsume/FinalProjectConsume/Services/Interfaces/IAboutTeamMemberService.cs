using FinalProjectConsume.Models.AboutAgency;
using FinalProjectConsume.Models.AboutTeamMember;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IAboutTeamMemberService
    {
        Task<IEnumerable<AboutTeamMember>> GetAllAsync();
        Task<AboutTeamMember> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(AboutTeamMemberCreate model);
        Task<HttpResponseMessage> EditAsync(int id, AboutTeamMemberEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
