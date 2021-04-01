using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using Syncfusion.Blazor;

namespace Syncfusion.HelpDesk.Client.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSyncfusionBlazor();
            //Syncfusion.Licensing.SyncfusionLicenseProvider
            //    .RegisterLicense("Enter Your Syncfusion License Here");
        }
    }
}
