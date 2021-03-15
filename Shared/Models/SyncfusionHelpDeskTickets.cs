using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace Syncfusion.HelpDesk.Models
{
    public class SyncfusionHelpDeskTickets : IAuditable
    {
        public SyncfusionHelpDeskTickets()
        {
            SyncfusionHelpDeskTicketDetails = new HashSet<SyncfusionHelpDeskTicketDetails>();
        }

        [Key]
        public int HelpDeskTicketId { get; set; }
        public int ModuleId { get; set; }
        public string TicketStatus { get; set; }

        [Required]
        public DateTime TicketDate { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage =
            "Description must be a minimum of 2 and maximum of 50 characters.")]
        public string TicketDescription { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual ICollection<SyncfusionHelpDeskTicketDetails> SyncfusionHelpDeskTicketDetails { get; set; }
    }
}