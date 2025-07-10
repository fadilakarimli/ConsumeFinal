using FinalProjectConsume.Models.Blog;
using FinalProjectConsume.Models.TeamMember;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface ITeamMemberService
    {
        Task<IEnumerable<TeamMember>> GetAllAsync();
        Task<TeamMember> GetByIdAsync(int id);
        Task<HttpResponseMessage> CreateAsync(TeamMemberCreate model);
        Task<HttpResponseMessage> EditAsync(int id, TeamMemberEdit model);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
