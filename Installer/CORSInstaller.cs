using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ner_pr_api.Installer
{
    public class CORSInstaller : IInstallers
    {
        public void InstallerService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {

                options.AddPolicy("AllowNerFrontend", builder =>
                   {
                       builder.WithOrigins(
                           "http://192.168.3.6:8080", "http://localhost:4200"
                       ).AllowAnyHeader()
                       .AllowAnyMethod();
                   });

                //options.AddPolicy("AllowOrigin", options => options.AllowAnyHeader().AllowAnyOrigin().AllowCredentials());
            });
        }
    }
}