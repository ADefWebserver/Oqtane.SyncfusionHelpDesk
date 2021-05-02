using System.Collections.Generic;
using System.Threading.Tasks;
using Syncfusion.Helpdesk.Models;

namespace Syncfusion.Helpdesk.Services
{
    public interface IHelpdeskService
    {
        Task<List<SyncfusionHelpDeskTickets>>
            GetSyncfusionHelpDeskTicketsByUserAsync(
            int ModuleId, string username);

        Task<SyncfusionHelpDeskTickets>
            GetSyncfusionHelpDeskTicketByUserAsync(
            int HelpDeskTicketId, int ModuleId, string username);

        Task<SyncfusionHelpDeskTickets>
            AddSyncfusionHelpDeskTicketsAsync(
            SyncfusionHelpDeskTickets SyncfusionHelpDeskTickets);

        Task<SyncfusionHelpDeskTickets>
            UpdateSyncfusionHelpDeskTicketsAsync(
            SyncfusionHelpDeskTickets SyncfusionHelpDeskTickets);
    }
}