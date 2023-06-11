using System.ComponentModel.DataAnnotations;

// **

namespace SportApplication.Data.Models
{
	public class Tournament : Entity
	{
		[Required]
		public string Title { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public DateTime StartingDate { get; set; }

		public DateTime EndingDate { get; set; }

		public Sport Sport { get; set; }

		public int SportId { get; set; }

		public List<Event> Events { get; set; } = new List<Event>();
	}
}
