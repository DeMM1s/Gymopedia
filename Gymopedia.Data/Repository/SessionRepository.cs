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
    }
}
