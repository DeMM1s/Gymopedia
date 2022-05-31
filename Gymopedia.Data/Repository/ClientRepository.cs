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
    }
}
