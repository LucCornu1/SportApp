using Microsoft.AspNetCore.Mvc;
using SportApplication.Models;
using SportApplication.Services;

namespace SportApplication.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
        {
			_accountService = accountService;
		}

        public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginForm_ViewModel model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View();
				}

				await _accountService.LoginAsync(model.Email, model.Password, model.RememberMe);

				return RedirectToAction("Index", "Home");
			} 
			catch (Exception e)
			{
				ModelState.AddModelError(string.Empty, e.Message); // Return an error to a specific part of a form
				return View();
			}
			
		}

		public async Task<IActionResult> Logout()
		{
			await _accountService.LogoutAsync();
			return RedirectToAction("Index", "Home");
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(AddUser_ViewModel model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View();
				}

                await _accountService.RegisterAsync(model);
                return RedirectToAction("Index", "Home");
            }
			catch (Exception e)
			{
				ModelState.AddModelError (string.Empty, e.Message);
				return View();
			}

			
		}
	}
}
