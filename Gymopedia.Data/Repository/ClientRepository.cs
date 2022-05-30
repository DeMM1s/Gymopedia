using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;

namespace Gymopedia.Data.Repository
{
    public class ClientRepository : Repository<ClientDbContext>, IClientRepository
    {
        public ClientRepository(IDbContextFactory<ClientDbContext> factory) : base(factory)
        {

        }

        public void Add(Client client)
        {
            Context.Clients.Add(client);
        }
    }
}
