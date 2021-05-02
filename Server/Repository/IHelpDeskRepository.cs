using System.Collections.Generic;
using System.Linq;
using Syncfusion.Helpdesk.Models;

namespace Syncfusion.Helpdesk.Repository
{
    public interface IHelpdeskRepository
    {
        IQueryable<Models.SyncfusionHelpDeskTickets>
            GetSyncfusionHelpDeskTickets(int ModuleId);

        Models.SyncfusionHelpDeskTickets
            GetSyncfusionHelpDeskTicket(int Id);

        Models.SyncfusionHelpDeskTickets
            AddSyncfusionHelpDeskTickets(
            Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTicket);

        Models.SyncfusionHelpDeskTickets
            UpdateSyncfusionHelpDeskTickets(
            string UpdateMode,
            Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTicket);

        void DeleteSyncfusionHelpDeskTickets(int Id);
    }
}