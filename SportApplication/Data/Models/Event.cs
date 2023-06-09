using System.ComponentModel.DataAnnotations;

// **

namespace SportApplication.Data.Models
{
	public class Event : Entity
	{
		[Required]
		[StringLength(40)]
		public string Title { get; set; } = string.Empty;

		[StringLength(250)]
		public string Description { get; set; } = string.Empty;

		[Required]
		public DateTime StartingDate { get; set; }

		[Required]
		public DateTime EndingDate { get; set;}

		public Tournament Tournament { get; set; }

		public int TournamentId { get; set; }

		public Constraint? Constraint { get; set; }

		public int? ConstraintId { get; set; }

		public List<Participation> Participations { get; set; } = new List<Participation>();
    }
}
