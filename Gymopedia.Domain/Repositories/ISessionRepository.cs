using Gymopedia.Domain.Models;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Domain.Repositories
{
    public interface ISessionRepository : IRepository
    {
        void Add(Session session);
        Task<List<Session>> GetAllByCoachId(long coachId, CancellationToken cancellationToken);
        Task<Session?> Get(int sessionId, CancellationToken cancellationToken);
        Task<Session?> Delete(int sessionId, CancellationToken cancellationToken);
    }
}
