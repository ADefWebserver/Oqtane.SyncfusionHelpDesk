using System.Collections.Generic;
using Syncfusion.HelpDesk.Models;

namespace Syncfusion.HelpDesk.Repository
{
    public interface IHelpDeskRepository
    {
        IEnumerable<Models.SyncfusionHelpDeskTickets> GetSyncfusionHelpDeskTickets(int ModuleId);
        Models.SyncfusionHelpDeskTickets GetSyncfusionHelpDeskTicket(int Id);
        Models.SyncfusionHelpDeskTickets AddSyncfusionHelpDeskTickets(Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTicket);
        Models.SyncfusionHelpDeskTickets UpdateSyncfusionHelpDeskTickets(Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTicket);
        void DeleteSyncfusionHelpDeskTickets(int Id);
    }
}
