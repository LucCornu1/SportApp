namespace SportApplication.Services
{
	public interface IAccountService
	{
		Task LoginAsync(string email, string password, bool rememberMe);

		Task LogoutAsync();
	}
}
