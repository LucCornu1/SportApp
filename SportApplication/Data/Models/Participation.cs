namespace SportApplication.Data.Models
{
	public class Participation : Entity
	{
		public User User { get; set; }

		public int UserId { get; set; }

		public Event Event { get; set; }

		public int EventId { get; set; }

		public int Rank { get; set; } = 0;

	}
}
