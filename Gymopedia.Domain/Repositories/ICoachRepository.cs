using Gymopedia.Domain.Models;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Domain.Repositories
{
    public interface ICoachRepository : IRepository
    {
        void Add(Coach coach);
        Task<Coach?> Get(long coachId, CancellationToken cancellationToken);
        Task<Coach?> GetFromName(string name, CancellationToken cancellationToken);
        Task<Coach?> Delete(int coachId, CancellationToken cancellationToken);
    }
}
