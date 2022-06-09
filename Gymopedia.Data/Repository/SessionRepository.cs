using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Repositories;
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
            return await Context.Sessions.SingleOrDefaultAsync(o => o.Id == sessionId, cancellationToken);
        }

        public async Task<Session?> Delete(int sessionId, CancellationToken cancellationToken)
        {
            Session? session = await Context.Sessions.SingleOrDefaultAsync(o => o.Id == sessionId, cancellationToken);
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
