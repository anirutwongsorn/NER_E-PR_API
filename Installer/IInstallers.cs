using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ner_pr_api.Installer
{
    public interface IInstallers
    {
        void InstallerService(IServiceCollection services, IConfiguration configration);
    }
}