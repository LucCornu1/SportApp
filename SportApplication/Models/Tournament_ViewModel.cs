namespace SportApplication.Models
{
	public class Tournament_ViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime StartingDate { get; set; }
		public DateTime EndingDate { get; set; }
		public string SportName { get; set; }
	}
}
