using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Syncfusion.Helpdesk.Models;
using Oqtane.Repository.Databases.Interfaces;

namespace Syncfusion.Helpdesk.Repository
{
    public class HelpdeskContext :DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual
            DbSet<Models.SyncfusionHelpDeskTickets>
            SyncfusionHelpDeskTickets
        { get; set; }

        public virtual
            DbSet<SyncfusionHelpDeskTicketDetails>
            SyncfusionHelpDeskTicketDetails
        { get; set; }

        public HelpdeskContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }
    }
}