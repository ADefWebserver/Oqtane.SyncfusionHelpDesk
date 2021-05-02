using Syncfusion.Helpdesk.Models;
using System.Threading.Tasks;

namespace Syncfusion.Helpdesk.Services
{
    public interface IHelpdeskAdminService
    {
        // Admin Methods

        Task<SyncfusionHelpDeskTickets>
            GetSyncfusionHelpDeskTicketAdminAsync(
            int HelpDeskTicketId, int ModuleId);

        Task<SyncfusionHelpDeskTickets>
            UpdateSyncfusionHelpDeskTicketsAdminAsync(
            SyncfusionHelpDeskTickets objSyncfusionHelpDeskTicket);

        Task DeleteSyncfusionHelpDeskTicketsAsync(
            int Id, int ModuleId);
    }
}