using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportApplication.Data.Models;
using SportApplication.Models;
using SportApplication.Services;

namespace SportApplication.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> Index(int id)
        {
            IEnumerable<TournamentEvents_ViewModel> tournamentEventList = await _eventService.GetTournamentEventsAsync(id);

            return View(tournamentEventList);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create(int id)
        {
            var model = new CreateEvent_ViewModel()
            {
                TournamentId = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, string description, DateTime startingDate, DateTime endingDate, int tournamentId)
        {
            CreateEvent_ViewModel viewModel = new CreateEvent_ViewModel()
            {
                Title = title,
                Description = description,
                StartingDate = startingDate,
                EndingDate = endingDate,
                TournamentId = tournamentId
            };

            try
            {
                await _eventService.CreateEventAsync(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
