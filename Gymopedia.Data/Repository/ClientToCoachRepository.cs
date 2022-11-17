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

        public async Task<ClientToCoach?> Get(long clientId,long coachId , CancellationToken cancellationToken)
        {
            return await Context.ClientToCoach.FirstOrDefaultAsync(o => o.ClientId == clientId && o.CoachId == coachId, cancellationToken);
        }

        public async Task<List<ClientToCoach>> GetAll(long clientId, CancellationToken cancellationToken)
        {
            var data = Context.ClientToCoach.Where(o => o.ClientId == clientId);
            var List = new List<ClientToCoach>();
            foreach(var item in data)
            {
                List.Add(new ClientToCoach
                { 
                    ClientId = item.ClientId,
                    Id = item.Id,
                    CoachId = item.CoachId
                });
            }
            return List;
        }

        public async Task<ClientToCoach?> Delete(long clientId, long coachId, CancellationToken cancellationToken)
        {
            ClientToCoach? ClientToCoach = await Context.ClientToCoach.FirstOrDefaultAsync(o => o.ClientId == clientId, cancellationToken);
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
