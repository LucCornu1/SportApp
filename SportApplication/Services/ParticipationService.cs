using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportApplication.Data;
using SportApplication.Data.Models;
using SportApplication.Models;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SportApplication.Services
{
    public class ParticipationService : IParticipationService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _accessor;

        public ParticipationService(AppDbContext appDbContext, IHttpContextAccessor accessor)
        {
            _appDbContext = appDbContext;
            _accessor = accessor;
        }

        public async Task<List<GetUserEvents_ViewModel>> GetUserParticipationsAsync()
        {
            var userId = _accessor.HttpContext.User.FindFirst("Id").Value;

            var userEventsId = from Participation in _appDbContext.Participations
                        where Participation.UserId == int.Parse(userId)
                        select Participation.EventId;

            List<GetUserEvents_ViewModel> UserEventsList = new List<GetUserEvents_ViewModel>();

            if (userEventsId is null)
            {
                return UserEventsList;
            }
            
            foreach(var userEventId in userEventsId)
            {
                var result = from Event in _appDbContext.Events
                             where Event.Id == userEventId
                             select Event;

                UserEventsList.Add(new GetUserEvents_ViewModel(result.First(), userEventId));
            }

            return UserEventsList;
        }

        public async Task DeleteUserParticipationAsync(GetUserEvents_ViewModel userEvent)
        {
            var userId = _accessor.HttpContext.User.FindFirst("Id").Value;

            var participation = from Participation in _appDbContext.Participations
                                where Participation.EventId == userEvent.ParticipationId
                                select Participation;

            participation = participation.Where(x => x.UserId == int.Parse(userId));

            _appDbContext.Participations.Remove(participation.First());
            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateUserParticipationAsync(int id)
        {
            var userId = _accessor.HttpContext.User.FindFirst("Id").Value;

            await _appDbContext.Participations.AddAsync(new Participation
            {
                EventId = id,
                UserId = int.Parse(userId)
            });

            await _appDbContext.SaveChangesAsync();
        }
    }
}
