using static SportApplication.Infrastructure.Enumerations;

namespace SportApplication.Data.Models
{
	public class Constraint : Entity
	{
		public DateTime? MinimumBirthdate { get; set; }

		public DateTime? MaximumBirthdate { get; set; }

		public int? MaxParticipantCount { get; set; }

		public CompetitionGender Gender { get; set; }

		public List<Event> Events { get; set; } = new List<Event>();
	}
}
