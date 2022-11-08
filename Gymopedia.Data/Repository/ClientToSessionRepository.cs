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


        public async Task<ClientToSession?> Delete(long clientId, long sessionId, CancellationToken cancellationToken)
        {
            ClientToSession? ClientToSession = await Context.ClientToSession.FirstOrDefaultAsync(o => o.Id == clientId, cancellationToken);
            if (ClientToSession == null)
            {
                return null;
            }
            Context.ClientToSession.Remove(ClientToSession);
            Context.SaveChanges();
            return ClientToSession;
        }
    }
}
