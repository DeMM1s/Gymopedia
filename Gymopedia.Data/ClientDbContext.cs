using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Models;

namespace Gymopedia.Data
{
    public class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
        {

        }
        public DbSet<Client> Clients => Set<Client>();
    }
}
