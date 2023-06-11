using System.Security.Cryptography;
using System.Text;

// **

namespace SportApplication.Infrastructure
{
	public static class Helpers
	{
		public static async Task<bool> IsPasswordCorrectAsync(string password, string hash)
		{
			var result = hash.Equals(await HashPasswordAsync(password));
			return result;
		}

		public static async Task<string> HashPasswordAsync(string password)
		{
			if (string.IsNullOrEmpty(password))
				return string.Empty;

			using (SHA256 sha256 = SHA256.Create())
			{
				// Convert the password string to a byte array
				byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

				// Compute the hash value of the password bytes
				byte[] hashBytes = await sha256.ComputeHashAsync(new MemoryStream(passwordBytes));

				// Convert the hashed password bytes back to a string
				string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "");

				return hashedPassword;
			}
		}
	}
}
