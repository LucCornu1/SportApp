using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportApplication.Models;
using SportApplication.Services;

namespace SportApplication.Controllers
{
	public class TournamentController : Controller
	{
		private readonly ITournamentService _tournamentService;

		public TournamentController(ITournamentService tournamentService)
        {
			_tournamentService = tournamentService;
		}

        public async Task<IActionResult> Index()
		{
			IEnumerable<Tournament_ViewModel> result = await _tournamentService.GetNextTournamentsAsync();

			return View(result);
		}

		[HttpGet]
		[Authorize(Policy = "Admin")]
		public async Task<IActionResult> Create()
		{
			var model = new CreateTournament_ViewModel()
			{
				Sports = await _tournamentService.GetSportsAsync()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(string title, string description, DateTime startingDate, DateTime endingDate, int sport)
		{
			// Validation
			try
			{
				await _tournamentService.CreateTournamentAsync(sport, title, startingDate, endingDate);
			}
			catch(Exception ex)
			{
				// return BadRequest(ex.Message); // Return error 400 with error message

				ModelState.AddModelError(string.Empty, ex.Message); // Return an error to a specific part of a form
				return View();
			}
			
			// Creation

			return RedirectToAction(nameof(Index));
		}

		[HttpGet("{id:int}/CreateEvent")]
		public async Task<IActionResult> CreateEvent()
		{
			var model = new AddEvent_ViewModel()
			{
				Title = default,
				Description = default,
				StartingDate = default,
				EndingDate = default,
				TournamentId = default,
				TournamentTitle = default
			};

			return View(model);
		}
	}
}
