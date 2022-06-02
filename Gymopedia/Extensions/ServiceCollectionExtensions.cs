using Gymopedia.Domain.Repositories;
using Gymopedia.Data;
using Gymopedia.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Gymopedia.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            const string ConnectionName = "LocalDbConnection";
            var connectionString = configuration.GetConnectionString(ConnectionName);

            services.AddDbContext<LocalDbContext>(dbContextOptionsBuilder =>
            dbContextOptionsBuilder.UseNpgsql(connectionString));

            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<ICoachRepository, CoachRepository>();

            return services;
        }
        public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

    }
}
