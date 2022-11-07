using Gymopedia.Domain.Models;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Domain.Repositories
{
    public interface IClientRepository : IRepository
    {
        void Add(Client client);
        Task<Client?> Get(long clientId, CancellationToken cancellationToken);
        Task<Client?> Delete(long clientId, CancellationToken cancellationToken);
    }
}
