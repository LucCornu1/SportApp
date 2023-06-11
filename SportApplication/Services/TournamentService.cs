using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SportApplication.Data;
using SportApplication.Data.Models;
using SportApplication.Models;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("SportApplication.Tests")] // Make internal functions visible to unit tests
namespace SportApplication.Services
{
	public class TournamentService : ITournamentService
	{
		private readonly AppDbContext _appDbContext;

        public TournamentService(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }

        public async Task<int> CreateTournamentAsync(int sportId, string Title, DateTime startingDate, DateTime endingDate)
		{
			if (!CreateTournamentDataIsValid(sportId, Title, startingDate, endingDate))
			{
				throw new InvalidDataException("Invalid Data");
			}

			var result = await _appDbContext.Tournaments
				.AddAsync(
				new Tournament 
				{
					SportId = sportId,
					Title = Title,
					StartingDate = startingDate,
					EndingDate = endingDate
				});

			await _appDbContext.SaveChangesAsync();

			return result.Entity.Id;
		}

		internal bool CreateTournamentDataIsValid(int sportId, string title, DateTime startingDate, DateTime endingDate)
		{
			// Validate Sport Id
			if (sportId < 0 || !_appDbContext.Sports.Any(s => s.Id == sportId))
			{
				return false;
			}

			// Validate Name
			if (string.IsNullOrWhiteSpace(title))
			{
				return false;
			}

			// Validate startDate
			if (startingDate < DateTime.Now.AddDays(1))
			{
				return false;
			}

			// Validate endingDate
			if (endingDate <= startingDate)
			{
				return false;
			}

			return true;
		}


        public async Task<IEnumerable<Tournament_ViewModel>?> GetNextTournamentsAsync()
		{
			var itemList = await _appDbContext.Tournaments
				.Include(t => t.Sport)
				.Where(t => t.StartingDate > DateTime.Now)
				.ToListAsync();

			var result = itemList.Select(t => new Tournament_ViewModel()
			{
				Id = t.Id,
				Title = t.Title,
				StartingDate = t.StartingDate,
				EndingDate = t.EndingDate,
				SportName = t.Sport.Name
			});

			return result;
		}

		public async Task<List<(int id, string name)>> GetSportsAsync()
		{
			var sports = await _appDbContext.Sports.ToListAsync();

			var result = sports.Select(s => (s.Id, s.Name)).ToList();

			return result;
		}

        public Task CreateEventAsync(int TournamentId, string Title, DateTime startingDate, DateTime endingDate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TournamentEvents_ViewModel>> GetTournamentEventsAsync(int id)
        {
			IEnumerable<Event> results = await _appDbContext.Events
                .Where(e => e.TournamentId == id)
				.ToListAsync();

            if (!results.Any())
			{
				return new List<TournamentEvents_ViewModel>();
			}

			string tournamentTitle = (from Tournament in _appDbContext.Tournaments
									  where Tournament.Id == id
                                      select Tournament.Title).First();

            List<TournamentEvents_ViewModel> TournamentEventsList = new List<TournamentEvents_ViewModel>();

            foreach (var r in results)
			{
				TournamentEventsList.Add(new TournamentEvents_ViewModel(r, tournamentTitle));
			}

            return TournamentEventsList;
        }
    }
}
