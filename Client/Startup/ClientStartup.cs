using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using Syncfusion.Blazor;
namespace Syncfusion.Helpdesk.Client.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSyncfusionBlazor();
            //Syncfusion.Licensing.SyncfusionLicenseProvider
            //.RegisterLicense("Enter Your Syncfusion License Here");
        }
    }
}