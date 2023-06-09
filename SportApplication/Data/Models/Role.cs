namespace SportApplication.Data.Models
{
	public class Role : Entity
	{
		public string Name { get; set; } = string.Empty;

		public List<User> Users { get; set; } 
			= new List<User>();
	}
}
