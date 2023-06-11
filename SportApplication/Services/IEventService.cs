using SportApplication.Models;

namespace SportApplication.Services
{
    public interface IEventService
    {
        Task<int> CreateEventAsync(CreateEvent_ViewModel viewModel);
        Task<IEnumerable<TournamentEvents_ViewModel>> GetTournamentEventsAsync(int id);
    }
}
