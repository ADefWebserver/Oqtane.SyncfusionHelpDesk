using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using Syncfusion.HelpDesk.Models;

namespace Syncfusion.HelpDesk.Services
{
    public class HelpDeskAdminService : ServiceBase, IHelpDeskAdminService, IService
    {
        private readonly SiteState _siteState;

        public HelpDeskAdminService(HttpClient http, SiteState siteState) : base(http)
        {
            _siteState = siteState;
        }

        private string Apiurl => CreateApiUrl(_siteState.Alias, "HelpDeskAdmin");

        public async Task<Models.SyncfusionHelpDeskTickets> GetSyncfusionHelpDeskTicketAdminAsync(int HelpDeskTicketId, int ModuleId)
        {
            return await GetJsonAsync<Models.SyncfusionHelpDeskTickets>(CreateAuthorizationPolicyUrl($"{Apiurl}/{HelpDeskTicketId}", ModuleId));
        }

        public async Task<Models.SyncfusionHelpDeskTickets> UpdateSyncfusionHelpDeskTicketsAdminAsync(Models.SyncfusionHelpDeskTickets objSyncfusionHelpDeskTicket)
        {
            return await PutJsonAsync<Models.SyncfusionHelpDeskTickets>(CreateAuthorizationPolicyUrl($"{Apiurl}/{objSyncfusionHelpDeskTicket.HelpDeskTicketId}", objSyncfusionHelpDeskTicket.ModuleId), objSyncfusionHelpDeskTicket);
        }

        public async Task DeleteSyncfusionHelpDeskTicketsAsync(int HelpDeskId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{HelpDeskId}", ModuleId));
        }
    }
}
