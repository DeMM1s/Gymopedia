using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;

namespace Gymopedia.Data.Repository
{
    public class CoachWorkDayRepository : MasterRepository, ICoachWorkDayRepository
    {
        public CoachWorkDayRepository(LocalDbContext context) : base(context)
        {

        }
        public void Add(CoachWorkDay coachWorkDay)
        {
            Context.CoachWorkDays.Add(coachWorkDay);
        }
        public async Task<CoachWorkDay?> Get(int coachWorkDayId, CancellationToken cancellationToken)
        {
            return await Context.CoachWorkDays.FirstOrDefaultAsync(o => o.Id == coachWorkDayId, cancellationToken);
        }

        public async Task<CoachWorkDay?> Delete(int coachWorkDayId, CancellationToken cancellationToken)
        {
            CoachWorkDay? coachWorkDay = await Context.CoachWorkDays.FirstOrDefaultAsync(o => o.Id == coachWorkDayId, cancellationToken);
            if (coachWorkDay == null)
            {
                //throw new InvalidOperationException("Клиент с данным id не найден");
                return null;
            }
            Context.CoachWorkDays.Remove(coachWorkDay);
            Context.SaveChanges();
            return coachWorkDay;
        }
    }
}
