using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ner_api.Data;

namespace ner_pr_api.Installer
{
    public class DatabaseInstaller : IInstallers
    {
        public void InstallerService(IServiceCollection services, IConfiguration configration)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(configration.GetConnectionString("ConnectionSqlServer")));
        }
    }
}