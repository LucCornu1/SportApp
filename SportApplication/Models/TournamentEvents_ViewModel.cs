using Microsoft.AspNetCore.Mvc;
using SportApplication.Data.Models;

namespace SportApplication.Models
{
    public class TournamentEvents_ViewModel
    {
        public TournamentEvents_ViewModel()
        {
            
        }

        public TournamentEvents_ViewModel(Event e)
        {
            Id = e.Id;
            Title = e.Title;
            Description = e.Description;
            StartingDate = e.StartingDate;
            EndingDate = e.EndingDate;
        }

        public TournamentEvents_ViewModel(Event e, string tournamentTitle)
        {
            Id = e.Id;
            Title = e.Title;
            Description = e.Description;
            StartingDate = e.StartingDate;
            EndingDate = e.EndingDate;
            TournamentTitle = tournamentTitle;
        }

        [HiddenInput]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string? TournamentTitle { get; set; }
    }
}
