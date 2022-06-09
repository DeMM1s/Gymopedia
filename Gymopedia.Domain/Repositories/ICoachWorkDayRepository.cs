using Gymopedia.Domain.Models;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Domain.Repositories
{
    public interface ICoachWorkDayRepository : IRepository
    {
        void Add(CoachWorkDay coachWorkDay);
        Task<CoachWorkDay?> Get(int coachWorkDayId, CancellationToken cancellationToken);
        Task<CoachWorkDay?> Delete(int coachWorkDayId, CancellationToken cancellationToken);
    }
}
