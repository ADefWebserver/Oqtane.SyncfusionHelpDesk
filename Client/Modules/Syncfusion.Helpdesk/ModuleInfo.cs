using Oqtane.Models;
using Oqtane.Modules;

namespace Syncfusion.Helpdesk
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Helpdesk",
            Description = "Helpdesk",
            Version = "1.0.1",
            ServerManagerType = "Syncfusion.Helpdesk.Manager.HelpdeskManager, Syncfusion.Helpdesk.Server.Oqtane",
            ReleaseVersions = "1.0.0,1.0.1",
            Dependencies = "Syncfusion.Helpdesk.Shared.Oqtane,Syncfusion.Blazor,Syncfusion.ExcelExport.Net,Syncfusion.Licensing,Syncfusion.PdfExport.Net,Newtonsoft.Json"
        };
    }
}
