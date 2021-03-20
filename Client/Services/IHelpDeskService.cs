using System.Collections.Generic;
using System.Threading.Tasks;
using Syncfusion.HelpDesk.Models;

namespace Syncfusion.HelpDesk.Services
{
    public interface IHelpDeskService
    {
        Task<List<Models.SyncfusionHelpDeskTickets>> GetSyncfusionHelpDeskTicketsAdminAsync(int ModuleId);

        Task<List<Models.SyncfusionHelpDeskTickets>> GetSyncfusionHelpDeskTicketsByUserAsync(int ModuleId, string username);

        Task<Models.SyncfusionHelpDeskTickets> AddSyncfusionHelpDeskTicketsAsync(Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTickets);
        
        Task<Models.SyncfusionHelpDeskTickets> UpdateSyncfusionHelpDeskTicketsAsync(Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTickets);

        Task DeleteSyncfusionHelpDeskTicketsAsync(int Id, int ModuleId);
    }
}
