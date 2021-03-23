using System.Collections.Generic;
using System.Threading.Tasks;
using Syncfusion.HelpDesk.Models;

namespace Syncfusion.HelpDesk.Services
{
    public interface IHelpDeskAdminService
    {
        // Admin Methods

        Task<Models.SyncfusionHelpDeskTickets> GetSyncfusionHelpDeskTicketAdminAsync(int HelpDeskTicketId, int ModuleId);

        Task<Models.SyncfusionHelpDeskTickets> UpdateSyncfusionHelpDeskTicketsAdminAsync(Models.SyncfusionHelpDeskTickets objSyncfusionHelpDeskTicket);

        Task DeleteSyncfusionHelpDeskTicketsAsync(int Id, int ModuleId);
    }
}
