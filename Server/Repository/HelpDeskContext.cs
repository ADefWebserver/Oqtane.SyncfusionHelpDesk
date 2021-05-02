using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Syncfusion.Helpdesk.Models;

namespace Syncfusion.Helpdesk.Repository
{
    public class HelpdeskContext : DBContextBase, IService
    {
        public virtual
            DbSet<Models.SyncfusionHelpDeskTickets>
            SyncfusionHelpDeskTickets
        { get; set; }

        public virtual
            DbSet<SyncfusionHelpDeskTicketDetails>
            SyncfusionHelpDeskTicketDetails
        { get; set; }

        public HelpdeskContext(
            ITenantResolver tenantResolver, IHttpContextAccessor accessor) :
            base(tenantResolver, accessor)
        {
            // ContextBase handles multi-tenant database connections
        }
    }
}