using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ner_pr_api.Installer
{
    public class SwaggerInstaller : IInstallers
    {
        public void InstallerService(IServiceCollection services, IConfiguration configration)
        {
            services.AddSwaggerGen(c =>
          {
              c.SwaggerDoc("v1", new OpenApiInfo { Title = "nerubber_api", Version = "v1" });
          });
        }
    }
}