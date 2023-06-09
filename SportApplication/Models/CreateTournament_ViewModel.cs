namespace SportApplication.Models
{
	public class CreateTournament_ViewModel
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime StartingDate { get; set; }
		public DateTime EndingDate { get; set; }
		// public Dictionary<int, string> Sports { get; set; }
		public List<(int id, string name)> Sports { get; set; } // ** A list containing a tuple

	}
}
