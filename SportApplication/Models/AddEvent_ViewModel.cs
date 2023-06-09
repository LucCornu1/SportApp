using SportApplication.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace SportApplication.Models
{
    public class AddEvent_ViewModel
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartingDate { get; set; }

        public DateTime EndingDate { get; set; }

        public int TournamentId { get; set; }

        public string TournamentTitle { get; set; }

        public List<string> TournamentEvents { get; set; } = new List<string>();
    }
}
