using SportApplication.Data.Models;
using System.Security.Policy;

namespace SportApplication.Models
{
    public class GetUserEvents_ViewModel
    {
        private Event @event;

        public GetUserEvents_ViewModel()
        {
            
        }

        public GetUserEvents_ViewModel(GetUserEvents_ViewModel e)
        {
            EventTitle = e.EventTitle;
            Description = e.Description;
            StartingDate = e.StartingDate;
            EndingDate = e.EndingDate;
            ParticipationId = e.ParticipationId;
        }

        public GetUserEvents_ViewModel(Event e, int participationId)
        {
            EventTitle = e.Title;
            Description = e.Description;
            StartingDate = DateOnly.FromDateTime(e.StartingDate);
            EndingDate = DateOnly.FromDateTime(e.EndingDate);
            this.ParticipationId = participationId;
        }

        public string EventTitle { get; set; }

        public string Description { get; set; }

        public DateOnly StartingDate { get; set; }

        public DateOnly EndingDate { get; set; }

        public int ParticipationId { get; set; }
    }
}
