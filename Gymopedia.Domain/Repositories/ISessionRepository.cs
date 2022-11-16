using Gymopedia.Domain.Models;
using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Domain.Repositories
{
    public interface ISessionRepository : IRepository
    {
        void Add(Session session);
        Task<List<SessionDto>> GetAllByCoachId(long coachId, CancellationToken cancellationToken);
        Task<List<SessionDto>> GetHistoryByCoachId(long coachId, CancellationToken cancellationToken);

        Task<Session?> Get(int sessionId, CancellationToken cancellationToken);
        Task<Session?> GetNearestSession(long clientId, CancellationToken cancellationToken);
        Task<SessionDto?> GetNearestClient(long coachId, CancellationToken cancellationToken);
        Task<Session?> Delete(int sessionId, CancellationToken cancellationToken);
    }
}
