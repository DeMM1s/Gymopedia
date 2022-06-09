using Gymopedia.Domain.Models;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Domain.Repositories
{
    public interface IClientRepository : IRepository
    {
        void Add(Client client);
        Task<Client?> Get(int clientId, CancellationToken cancellationToken);
        Task<Client?> Delete (int clientId, CancellationToken cancellationToken);
    }
}
