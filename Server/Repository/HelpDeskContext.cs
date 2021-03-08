using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Syncfusion.HelpDesk.Models;

namespace Syncfusion.HelpDesk.Repository
{
    public class HelpDeskContext : DBContextBase, IService
    {
        public virtual DbSet<Models.SyncfusionHelpDeskTickets> SyncfusionHelpDeskTickets { get; set; }

        public HelpDeskContext(ITenantResolver tenantResolver, IHttpContextAccessor accessor) : base(tenantResolver, accessor)
        {
            // ContextBase handles multi-tenant database connections
        }
    }
}
