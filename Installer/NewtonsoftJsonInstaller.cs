using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ner_pr_api.Installer
{
    public class NewtonsoftJsonInstaller : IInstallers
    {
        public void InstallerService(IServiceCollection services, IConfiguration configration)
        {
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }
    }
}