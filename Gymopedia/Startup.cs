using MediatR;

using System.Reflection;
using Gymopedia.Core.Clients;
using Gymopedia.Core.Coaches;
using Gymopedia.Core.Sessions;
using Gymopedia.Extensions;
using Gymopedia.Core.ClientToCoachs;
using Gymopedia.Core.ClientToSessions;
using Telegram.Bot;
using Deployf.Botf;

namespace Gymopedia
{
    public class Startup
    {
        
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMediatR(GetMediatrAssemblies().ToArray());

            services.AddDatabase(_configuration);
            services.AddAppDependencies(_configuration);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddBotf("");
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseBotf();
        }

        private IEnumerable<Assembly> GetMediatrAssemblies()
        {
            yield return Assembly.GetAssembly(typeof(CreateClient.Request))!;
            yield return Assembly.GetAssembly(typeof(GetClient.Request))!;
            yield return Assembly.GetAssembly(typeof(EditClient.Request))!;
            yield return Assembly.GetAssembly(typeof(DeleteClient.Request))!;

            yield return Assembly.GetAssembly(typeof(CreateCoach.Request))!;
            yield return Assembly.GetAssembly(typeof(GetCoach.Request))!;

            yield return Assembly.GetAssembly(typeof(CreateSession.Request))!;
            yield return Assembly.GetAssembly(typeof(GetSession.Request))!;


            yield return Assembly.GetAssembly(typeof(SubscribeToSession.Request))!;
            yield return Assembly.GetAssembly(typeof(GetClientToSession.Request))!;


            yield return Assembly.GetAssembly(typeof(SubscribeToCoach.Request))!;
            yield return Assembly.GetAssembly(typeof(GetClientToCoach.Request))!;

        }
    }
}
