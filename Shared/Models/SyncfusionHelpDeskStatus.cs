using System.Collections.Generic;

namespace Syncfusion.Helpdesk.Models
{
    public class SyncfusionHelpDeskStatus
    {
        public string ID { get; set; }
        public string Text { get; set; }

        public static List<SyncfusionHelpDeskStatus> Statuses =
            new List<SyncfusionHelpDeskStatus>() {
        new SyncfusionHelpDeskStatus(){ ID= "New", Text= "New" },
        new SyncfusionHelpDeskStatus(){ ID= "Open", Text= "Open" },
        new SyncfusionHelpDeskStatus(){ ID= "Urgent", Text= "Urgent" },
        new SyncfusionHelpDeskStatus(){ ID= "Closed", Text= "Closed" },
        };
    }
}