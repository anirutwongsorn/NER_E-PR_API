using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ner_pr_api.Installer
{
    public class ControllerInstaller : IInstallers
    {
        public void InstallerService(IServiceCollection services, IConfiguration configration)
        {
            services.AddControllers();
        }
    }
}