using SportApplication.Data.Models;
using SportApplication.Models;

namespace SportApplication.Services
{
	public interface ITournamentService
	{
		// Indicateur : Type "///"
		/// <summary>
		/// The summary
		/// </summary>
		/// <param name="sport"></param>
		/// <param name="Title"></param>
		/// <param name="startingDate"></param>
		/// <param name="endingDate"></param>
		/// <returns>Returns the id of the newly created tournament</returns>
		Task<int> CreateTournamentAsync(int sportId, string Title, DateTime startingDate, DateTime endingDate);

		Task CreateEventAsync(int TournamentId, string Title, DateTime startingDate, DateTime endingDate);
		Task<IEnumerable<Tournament_ViewModel>?> GetNextTournamentsAsync();
		Task<List<(int id, string name)>> GetSportsAsync();
        Task<IEnumerable<TournamentEvents_ViewModel>> GetTournamentEventsAsync(int id);
    }
}
