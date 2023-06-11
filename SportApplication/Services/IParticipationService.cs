using SportApplication.Models;

namespace SportApplication.Services
{
    public interface IParticipationService
    {
        Task<List<GetUserEvents_ViewModel>> GetUserParticipationsAsync();

        Task CreateUserParticipationAsync(int id);

        Task DeleteUserParticipationAsync(GetUserEvents_ViewModel userEvent);
    }
}
