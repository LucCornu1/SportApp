using Microsoft.AspNetCore.Mvc;

namespace SportApplication.Models
{
    public class CreateEvent_ViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public int TournamentId { get; set; }
    }
}
