using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;

namespace Gymopedia.Data.Repository
{
    public class ClientToSessionRepository: MasterRepository, IClientToSessionRepository
    {
        public ClientToSessionRepository(LocalDbContext context) : base(context)
        {

        }

        public void Add(ClientToSession ClientToSession)
        {
            Context.ClientToSession.Add(ClientToSession);
        }

        public async Task<ClientToSession?> Get(long clientId, CancellationToken cancellationToken)
        {
            return await Context.ClientToSession.FirstOrDefaultAsync(o => o.Id == clientId, cancellationToken);
        }

        public async Task<List<ClientToSession>> GetAll(long clientId, CancellationToken cancellationToken)
        {
            var data = Context.ClientToSession.Where(o => o.ClientId == clientId);
            var List = new List<ClientToSession>();
            foreach (var item in data)
            {
                List.Add(new ClientToSession
                {
                    ClientId = item.ClientId,
                    Id = item.Id,
                    SessionId = item.SessionId
                });
            }
            return List;
        }

        public async Task<List<ClientToSession>> GetAllBySession(long sessionId, CancellationToken cancellationToken)
        {
            var data = Context.ClientToSession.Where(o => o.SessionId == sessionId);
            var List = new List<ClientToSession>();
            foreach (var item in data)
            {
                List.Add(new ClientToSession
                {
                    ClientId = item.ClientId,
                    Id = item.Id,
                    SessionId = item.SessionId
                });
            }
            return List;
        }

        public async Task Delete(long clientId, long sessionId, CancellationToken cancellationToken)
        {
            ClientToSession ClientToSession = await Context.ClientToSession.FirstOrDefaultAsync(o => o.ClientId == clientId && o.SessionId == sessionId, cancellationToken);

            Context.ClientToSession.Remove(ClientToSession);
            Context.SaveChanges();
        }
    }
}
