using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportApplication.Models;
using SportApplication.Services;
using System.Security.Claims;

namespace SportApplication.Controllers
{
    public class ParticipationController : Controller
    {
        private readonly IParticipationService _participationService;

        public ParticipationController(IParticipationService participationService)
        {
            _participationService = participationService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var results = await _participationService.GetUserParticipationsAsync();
            return View(results);
        }

        [Authorize]
        public async Task<IActionResult> Create(int id)
        {
            await _participationService.CreateUserParticipationAsync(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Delete(GetUserEvents_ViewModel userEvent)
        {
            await _participationService.DeleteUserParticipationAsync(userEvent);
            // var results = await _participationService.GetUserParticipationsAsync();
            return RedirectToAction("Index");
        }
    }
}
