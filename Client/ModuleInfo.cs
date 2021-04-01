using Oqtane.Models;
using Oqtane.Modules;

namespace Syncfusion.HelpDesk
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Syncfusion HelpDesk",
            Description = "Syncfusion HelpDesk",
            Version = "1.0.0",
            ServerManagerType = "Syncfusion.HelpDesk.Manager.HelpDeskManager, Syncfusion.HelpDesk.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "Syncfusion.HelpDesk.Shared.Oqtane,Syncfusion.Blazor,Syncfusion.ExcelExport.Net,Syncfusion.Licensing,Syncfusion.PdfExport.Net,Newtonsoft.Json"
        };
    }
}
