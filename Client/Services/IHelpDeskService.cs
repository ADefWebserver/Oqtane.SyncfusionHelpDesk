using System.Collections.Generic;
using System.Threading.Tasks;
using Syncfusion.HelpDesk.Models;

namespace Syncfusion.HelpDesk.Services
{
    public interface IHelpDeskService
    {
        Task<List<Models.SyncfusionHelpDeskTickets>> GetSyncfusionHelpDeskTicketsByUserAsync(int ModuleId, string username);

        Task<Models.SyncfusionHelpDeskTickets> GetSyncfusionHelpDeskTicketByUserAsync(int HelpDeskTicketId, int ModuleId, string username);

        Task<Models.SyncfusionHelpDeskTickets> AddSyncfusionHelpDeskTicketsAsync(Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTickets);
        
        Task<Models.SyncfusionHelpDeskTickets> UpdateSyncfusionHelpDeskTicketsAsync(Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTickets);
    }
}
