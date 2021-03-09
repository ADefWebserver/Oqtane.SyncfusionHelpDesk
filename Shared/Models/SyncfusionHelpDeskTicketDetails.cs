using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace Syncfusion.HelpDesk.Models
{
    public partial class SyncfusionHelpDeskTicketDetails : IAuditable
    {
        public int Id { get; set; }
        public int HelpDeskTicketId { get; set; }
        public DateTime TicketDetailDate { get; set; }
        public string TicketDescription { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual SyncfusionHelpDeskTickets HelpDeskTicket { get; set; }
    }
}