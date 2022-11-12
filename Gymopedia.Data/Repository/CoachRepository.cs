using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;

namespace Gymopedia.Data.Repository
{
    public class CoachRepository : MasterRepository, ICoachRepository
    {
        public CoachRepository(LocalDbContext context) : base(context)
        {

        }
        public void Add(Coach coach)
        {
            Context.Coaches.Add(coach);
        }

        public async Task<Coach?> Get(long coachId, CancellationToken cancellationToken)
        {
            return await Context.Coaches.FirstOrDefaultAsync(o => o.ChatId == coachId, cancellationToken);
        }

        public async Task<Coach?> GetFromName(string name, CancellationToken cancellationToken)
        {
            return await Context.Coaches.FirstOrDefaultAsync(o => o.Name == name, cancellationToken);
        }

        public async Task<Coach?> Delete(int coachId, CancellationToken cancellationToken)
        {
            Coach? coach = await Context.Coaches.FirstOrDefaultAsync(o => o.ChatId == coachId, cancellationToken);

            Context.Coaches.Remove(coach);
            Context.SaveChanges();
            return coach;
        }
    }
}