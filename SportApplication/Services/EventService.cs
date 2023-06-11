using SportApplication.Data.Models;
using SportApplication.Data;
using SportApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace SportApplication.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _appDbContext;

        public EventService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> CreateEventAsync(CreateEvent_ViewModel viewModel)
        {
            if (!CreateEventDataIsValid(viewModel))
            {
                throw new InvalidDataException("Invalid Data");
            }

            var result = await _appDbContext.Events
                .AddAsync(
                new Event
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    StartingDate = viewModel.StartingDate,
                    EndingDate = viewModel.EndingDate,
                    TournamentId = viewModel.TournamentId,
                });

            await _appDbContext.SaveChangesAsync();

            return result.Entity.Id;
        }

        internal bool CreateEventDataIsValid(CreateEvent_ViewModel viewModel)
        {
            // Validate Sport Id
            if (viewModel.TournamentId < 0 || !_appDbContext.Tournaments.Any(t => t.Id == viewModel.TournamentId))
            {
                return false;
            }

            // Validate Name
            if (string.IsNullOrWhiteSpace(viewModel.Title))
            {
                return false;
            }

            // Validate startDate
            if (viewModel.StartingDate < DateTime.Now.AddDays(1))
            {
                return false;
            }

            // Validate endingDate
            if (viewModel.EndingDate <= viewModel.StartingDate)
            {
                return false;
            }

            return true;
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
