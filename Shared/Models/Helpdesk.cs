using System;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace Syncfusion.Helpdesk.Models
{
    [Table("SyncfusionHelpdesk")]
    public class Helpdesk : IAuditable
    {
        public int HelpdeskId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
