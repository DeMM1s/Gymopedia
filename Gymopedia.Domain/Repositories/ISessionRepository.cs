using Gymopedia.Domain.Models;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Domain.Repositories
{
    public interface ISessionRepository
    {
        void Add(Session session);

        //Task<CoachWorkDay?> Get(int coachId, CancellationToken cancellationToken);
    }
}
