using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;

namespace Gymopedia.Data.Repository
{
    public class CoachRepository: MasterRepository, ICoachRepository
    {
        public CoachRepository(LocalDbContext context) : base(context)
        {
                
        }
        public void Add(Coach coach)
        {
            Context.Coaches.Add(coach);
        }
        public async Task<Coach?> Get(int coachId, CancellationToken cancellationToken)
        {
            return await Context.Coaches.SingleOrDefaultAsync(o => o.Id == coachId, cancellationToken);
        }
    }
}
