using Gymopedia.Domain.Models;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Domain.Repositories
{
    public interface IClientToSessionRepository: IRepository
    {
        void Add(ClientToSession ClientToSession);
        Task<ClientToSession?> Get(int clientId, int sessionId, CancellationToken cancellationToken);
        Task<ClientToSession?> Delete(int clientId, int sessionId, CancellationToken cancellationToken);
    }
}
