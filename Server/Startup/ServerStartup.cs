using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using Syncfusion.Blazor;
using Microsoft.Extensions.Configuration;
namespace Syncfusion.Helpdesk.Server.Startup
{
    public class ServerStartup : IServerStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSyncfusionBlazor();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Register Syncfusion license
            // Get a free license here:
            // https://www.syncfusion.com/products/communitylicense
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false,
                reloadOnChange: true);
            var Configuration = builder.Build();
            var SyncfusionLicense = Configuration.GetSection("SyncfusionLicense");
            if (SyncfusionLicense != null)
            {
                Syncfusion.Licensing.SyncfusionLicenseProvider
                    .RegisterLicense(SyncfusionLicense.Value);
            }
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
        }
    }
}