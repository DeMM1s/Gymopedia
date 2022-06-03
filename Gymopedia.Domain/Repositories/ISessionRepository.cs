using Gymopedia.Domain.Models;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Domain.Repositories
{
    public interface ISessionRepository : IRepository
    {
        void Add(Session session);

        //Task<CoachWorkDay?> Get(int coachId, CancellationToken cancellationToken);
    }
}
