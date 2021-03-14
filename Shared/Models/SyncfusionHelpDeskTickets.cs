using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace Syncfusion.HelpDesk.Models
{
    public partial class SyncfusionHelpDeskTickets : IAuditable
    {
        public SyncfusionHelpDeskTickets()
        {
            SyncfusionHelpDeskTicketDetails = new HashSet<SyncfusionHelpDeskTicketDetails>();
        }

        public int Id { get; set; }
        public int ModuleId { get; set; }

        public string TicketStatus { get; set; }

        public DateTime TicketDate { get; set; }

        public string TicketDescription { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual ICollection<SyncfusionHelpDeskTicketDetails> SyncfusionHelpDeskTicketDetails { get; set; }
    }
}