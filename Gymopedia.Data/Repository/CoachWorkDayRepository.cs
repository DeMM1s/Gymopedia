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
    }
}
