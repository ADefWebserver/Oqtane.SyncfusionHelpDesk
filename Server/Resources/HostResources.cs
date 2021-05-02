using System.Collections.Generic;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Shared;
namespace Syncfusion.Helpdesk
{
    public class HostResources : IHostResources
    {
        public List<Resource> Resources => new List<Resource>()
        {
	        // Only register .css files
            // The JavaScript files will automatically be pulled
            // from the _content/syncfusion.blazor directory
            // so they do not need to be registered here
            new Resource {
                ResourceType = ResourceType.Stylesheet,
                Url = "_content/Syncfusion.Blazor/" +
                "styles/bootstrap4.css" },
            new Resource {
                ResourceType = ResourceType.Stylesheet,
                Url = "_content/Syncfusion.Blazor/" +
                "styles/material-dark.css" },
            new Resource {
                ResourceType = ResourceType.Script,
                Url = "https://kit.fontawesome.com/a076d05399.js" }
        };
    }
}