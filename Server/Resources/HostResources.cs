using System.Collections.Generic;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Shared;

namespace Syncfusion.HelpDesk
{
    public class HostResources : IHostResources
    {
        public List<Resource> Resources => new List<Resource>()
        {
            // The JavaScript files will automatically be pulled
            // from the _content/syncfusion.blazor directory
            // so it does not need to be registered here
            new Resource {
                ResourceType = ResourceType.Stylesheet,
                Url = "_content/Syncfusion.Blazor/" +
                "styles/bootstrap4.css" }
        };
    }
}