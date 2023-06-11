using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SportApplication.Data;
using SportApplication.Data.Models;
using SportApplication.Infrastructure;
using SportApplication.Models;
using System.Security.Claims;
using System.Security.Principal;

namespace SportApplication.Services
{
	public class AccountService : IAccountService
	{
		private readonly IHttpContextAccessor _accessor;
		private readonly AppDbContext _dbContext;

		public AccountService(IHttpContextAccessor accessor, AppDbContext dbContext)
        {
			_accessor = accessor;
			_dbContext = dbContext;
		}

		public async Task LoginAsync(string email, string password, bool rememberMe)
		{
			var user = _dbContext.Users
				.Include(u => u.Roles)
				.FirstOrDefault(u => u.Email == email);

			if (user is null)
			{
				throw new Exception("Not Found");
			}

			if (!await Helpers.IsPasswordCorrectAsync(password, user.HashedPassword))
			{
				throw new Exception("Incorrect Credentials");
			}

			var claims = new List<Claim>()
			{
				new Claim("Id", user.Id.ToString()),
				new Claim("Firstname", user.Firstname),
				new Claim("Lastname", user.Lastname),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Gender, user.Gender.ToString()),
				// new Claim(ClaimTypes.DateOfBirth, user.Birthdate.ToString("O"))
			};

			foreach(var role in user.Roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role.Name));
			}

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await _accessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties()
                {
                    IsPersistent = rememberMe
                });
        }

		public async Task LogoutAsync()
		{
			if (_accessor.HttpContext!.User.Identity!.IsAuthenticated)
			{
				await _accessor.HttpContext.SignOutAsync();
			}
		}

		internal async Task CheckEmail(string email)
		{
			var user = await _dbContext.Users
				.FirstOrDefaultAsync(x => x.Email == email);

            if (user != null)
			{
				throw new Exception("User already exists");
			}
        }

        public async Task RegisterAsync(AddUser_ViewModel model)
        {
			await CheckEmail(model.Email);

			try
			{
                List<Role> roles = new List<Role>();
                var role = await _dbContext.Roles.FindAsync(1);

				if (role != null)
				{
					roles.Add(role);
				}
                
                await _dbContext.Users
					.AddAsync(new User
					{
						Email = model.Email,
						HashedPassword = await Helpers.HashPasswordAsync(model.Password),
						Firstname = model.Firstname,
						Lastname = model.Lastname,
						Roles = roles
					});

				await _dbContext.SaveChangesAsync();

				await LoginAsync(model.Email, model.Password, false);
            }
			catch (Exception e)
			{
                throw e;
            }
        }
    }
}
