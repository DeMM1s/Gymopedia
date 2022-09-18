using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;

namespace Gymopedia.Data.Repository
{
    public class ClientRepository : MasterRepository, IClientRepository
    {
        public ClientRepository(LocalDbContext context) : base(context)
        {

        }

        public void Add(Client client)
        {
            Context.Clients.Add(client);
        }

        public async Task<Client?> Get(int clientId, CancellationToken cancellationToken)
        {
            return await Context.Clients.FirstOrDefaultAsync(o => o.Id == clientId, cancellationToken);
        }

        public async Task<Client?> Delete(int clientId, CancellationToken cancellationToken)
        {
            Client? client = await Context.Clients.SingleOrDefaultAsync(o => o.Id == clientId, cancellationToken);
            if (client == null)
            {
                return null;
            }
            Context.Clients.Remove(client);
            Context.SaveChanges();
            return client;
        }

    }
}