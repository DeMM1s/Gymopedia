using Gymopedia.Domain.Models;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Domain.Repositories
{
    public interface IClientToSessionRepository: IRepository
    {
        void Add(ClientToSession ClientToSession);
        Task<ClientToSession?> Get(long clientId, CancellationToken cancellationToken);
        Task<List<ClientToSession>> GetAll(long clientId, CancellationToken cancellationToken);
        Task Delete(long clientId, long sessionId, CancellationToken cancellationToken);
    }
}
