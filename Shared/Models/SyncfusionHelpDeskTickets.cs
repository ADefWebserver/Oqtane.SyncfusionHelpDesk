using System;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace Syncfusion.HelpDesk.Models
{
    [Table("SyncfusionHelpDeskTickets")]
    public class SyncfusionHelpDeskTickets : IAuditable
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string TicketStatus { get; set; }
        public DateTime TicketDate { get; set; }
        public string TicketDescription { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
