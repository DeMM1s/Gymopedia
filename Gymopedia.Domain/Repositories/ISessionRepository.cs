using Gymopedia.Domain.Models;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Domain.Repositories
{
    public interface ISessionRepository : IRepository
    {
        void Add(Session session);
        Task<Session?> Get(int sessionId, CancellationToken cancellationToken);
        Task<Session?> Delete(int sessionId, CancellationToken cancellationToken);
    }
}
