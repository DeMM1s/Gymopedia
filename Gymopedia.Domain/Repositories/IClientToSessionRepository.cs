using Gymopedia.Domain.Models;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Domain.Repositories
{
    public interface IClientToSessionRepository: IRepository
    {
        void Add(ClientToSession ClientToSession);
        Task<ClientToSession?> Get(long clientId, long sessionId, CancellationToken cancellationToken);
        Task<ClientToSession?> Delete(long clientId, long sessionId, CancellationToken cancellationToken);
    }
}
