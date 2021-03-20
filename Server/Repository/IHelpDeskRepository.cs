using System.Collections.Generic;
using System.Linq;
using Syncfusion.HelpDesk.Models;

namespace Syncfusion.HelpDesk.Repository
{
    public interface IHelpDeskRepository
    {
        IQueryable<Models.SyncfusionHelpDeskTickets> GetSyncfusionHelpDeskTickets(int ModuleId);
        Models.SyncfusionHelpDeskTickets GetSyncfusionHelpDeskTicket(int Id);
        Models.SyncfusionHelpDeskTickets AddSyncfusionHelpDeskTickets(Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTicket);
        Models.SyncfusionHelpDeskTickets UpdateSyncfusionHelpDeskTickets(string UpdateMode, Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTicket);
        void DeleteSyncfusionHelpDeskTickets(int Id);
    }
}
