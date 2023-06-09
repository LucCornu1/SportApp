using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SportApplication.Data;
using SportApplication.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// ** Add service for dbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
	var dir = AppDomain.CurrentDomain.BaseDirectory;
	var db = Path.Combine(dir, "data.db");
	options.UseSqlite($"DataSource={db};");
});

// ** Add the service for dependcy injection
builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddHttpContextAccessor();
// ** Add service for connection through cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Account/Login";
		options.LogoutPath = "/Account/Logout";
	});
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
	options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
});
// **

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ** Use Authentication
app.UseAuthentication();
// **
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
