using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;

namespace Gymopedia.Data.Repository
{
    public class ClientToCoachRepository: MasterRepository, IClientToCoachRepository
    {
        public ClientToCoachRepository(LocalDbContext context) : base(context)
        {

        }

        public void Add(ClientToCoach ClientToCoach)
        {
            Context.ClientToCoach.Add(ClientToCoach);
        }

        public async Task<ClientToCoach?> Get(long clientId, CancellationToken cancellationToken)
        {
            return await Context.ClientToCoach.FirstOrDefaultAsync(o => o.Id == clientId, cancellationToken);
        }

        public async Task<ClientToCoach?> Delete(long clientId, long coachId, CancellationToken cancellationToken)
        {
            ClientToCoach? ClientToCoach = await Context.ClientToCoach.SingleOrDefaultAsync(o => o.Id == clientId, cancellationToken);
            if (ClientToCoach == null)
            {
                return null;
            }
            Context.ClientToCoach.Remove(ClientToCoach);
            Context.SaveChanges();
            return ClientToCoach;
        }
    }
}
