using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Models;

namespace Gymopedia.Data
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
        {

        }
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Coach> Coaches => Set<Coach>();
        public DbSet<Session> Sessions => Set<Session>();
        public DbSet<CoachWorkDay> CoachWorkDays => Set<CoachWorkDay>();

    }
}
