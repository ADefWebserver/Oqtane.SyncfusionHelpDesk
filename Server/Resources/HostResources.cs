using System.Collections.Generic;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Shared;

namespace Syncfusion.HelpDesk
{
    public class HostResources : IHostResources
    {           
        // Register the .js and .css files 
        // The JavaScript files will automatically be pulled
        // from the _content/syncfusion.blazor directory
        // so it does not need to be registered here
        public List<Resource> Resources => new List<Resource>()
        {
            new Resource {
                ResourceType = ResourceType.Stylesheet,
                Url = "_content/syncfusion.blazor/" +
                "styles/bootstrap4.css" },
            new Resource {
                ResourceType = ResourceType.Stylesheet,
                Url = "_content/syncfusion.blazor/" +
                "styles/material-dark.css" },
            new Resource {
                ResourceType = ResourceType.Script,
                Url = "https://kit.fontawesome.com/a076d05399.js" }
        };
    }
}