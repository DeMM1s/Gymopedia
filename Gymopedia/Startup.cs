using MediatR;

using Gymopedia.Extensions;

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

            //services.AddMediatR(GetMediatrAssemblies().ToArray());

            services.AddAppDependencies(_configuration);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
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
        }

        /*private IEnumerable<Assembly> GetMediatrAssemblies()
        {
            yield return Assembly.GetAssembly(typeof(CreateProduct.Request))!;
        }*/
    }
}
