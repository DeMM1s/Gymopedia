using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Models;

namespace Gymopedia.Data.Repository
{
    public class SessionRepository : MasterRepository, ISessionRepository
    {
        public SessionRepository(LocalDbContext context) : base(context)
        {

        }

        public void Add(Session session)
        {
            Context.Sessions.Add(session);
        }
        public async Task<Session?> Get(int sessionId, CancellationToken cancellationToken)
        {
            return await Context.Sessions.FirstOrDefaultAsync(o => o.Id == sessionId, cancellationToken);
        }

        public async Task<Session?> GetNearestSession(long clientId, CancellationToken cancellationToken)
        {
            DateTime time = DateTime.UtcNow;
            var session = (from s in Context.Sessions
                           join c in Context.ClientToSession on s.Id equals c.SessionId
                           where c.ClientId == clientId && s.From > time
                           orderby s.From
                           select new
                           { 
                               Id = s.Id,
                               From = s.From,
                               CoachId = s.CoachId,
                           }).Take(1);
            if (session.Any())
            {
                Session response = new Session
                {
                    Id = session.First().Id,
                    From = session.First().From,
                    CoachId = session.First().CoachId
                };
                return response;
            }
            else return null;
            
        }

        public async Task<SessionDto?> GetNearestClient(long coachId, CancellationToken cancellationToken)
        {
            DateTime time = DateTime.UtcNow;
            var session = (from s in Context.Sessions
                           join c in Context.ClientToSession on s.Id equals c.SessionId
                           where s.CoachId == coachId && s.From > time
                           orderby s.From
                           select new
                           {
                               Id = s.Id,
                               From = s.From,
                               CoachId = s.CoachId,
                           }).Take(1);
            if (session.Any())
            {
                SessionDto response = new SessionDto
                {
                    Id = session.First().Id,
                    From = session.First().From,
                    CoachId = session.First().CoachId
                };
                return response;
            }
            else return null;

        }

        public async Task<List<SessionDto>> GetAllByCoachId(long coachId, CancellationToken cancellationToken)
        {
            var time = DateTime.UtcNow;
            var data = Context.Sessions.Where(o => o.CoachId == coachId && o.From > time);
            var List = new List<SessionDto>();
            foreach (var item in data)
            {
                List.Add(new SessionDto
                {
                    Id = item.Id,
                    CoachId = item.CoachId,
                    From = item.From
                }) ;
            }
            return List;
        }

        public async Task<List<SessionDto>> GetHistoryByCoachId(long coachId, CancellationToken cancellationToken)
        {
            var data = Context.Sessions.Where(o => o.CoachId == coachId);
            var List = new List<SessionDto>();
            foreach (var item in data)
            {
                List.Add(new SessionDto
                {
                    Id = item.Id,
                    CoachId = item.CoachId,
                    From = item.From
                });
            }
            return List;
        }

        public async Task<Session?> Delete(int sessionId, CancellationToken cancellationToken)
        {
            Session? session = await Context.Sessions.FirstOrDefaultAsync(o => o.Id == sessionId, cancellationToken);
            if (session == null)
            {
                //throw new InvalidOperationException("Клиент с данным id не найден");
                return null;
            }
            Context.Sessions.Remove(session);
            Context.SaveChanges();
            return session;
        }
    }
}
