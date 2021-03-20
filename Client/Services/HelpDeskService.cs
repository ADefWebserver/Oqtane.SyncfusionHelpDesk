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
    public class HelpDeskService : ServiceBase, IHelpDeskService, IService
    {
        private readonly SiteState _siteState;

        public HelpDeskService(HttpClient http, SiteState siteState) : base(http)
        {
            _siteState = siteState;
        }

         private string Apiurl => CreateApiUrl(_siteState.Alias, "HelpDesk");

        public async Task<List<Models.SyncfusionHelpDeskTickets>> GetSyncfusionHelpDeskTicketsAdminAsync(int ModuleId)
        {
            return await GetJsonAsync<List<Models.SyncfusionHelpDeskTickets>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", ModuleId));
        }
        public async Task<List<Models.SyncfusionHelpDeskTickets>> GetSyncfusionHelpDeskTicketsByUserAsync(int ModuleId, string username)
        {
            return await GetJsonAsync<List<Models.SyncfusionHelpDeskTickets>>(CreateAuthorizationPolicyUrl($"{Apiurl}?username={username}", ModuleId));
        }

        public async Task<Models.SyncfusionHelpDeskTickets> AddSyncfusionHelpDeskTicketsAsync(Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTickets)
        {
            return await PostJsonAsync<Models.SyncfusionHelpDeskTickets>(CreateAuthorizationPolicyUrl($"{Apiurl}", SyncfusionHelpDeskTickets.ModuleId), SyncfusionHelpDeskTickets);
        }

        public async Task AddSyncfusionHelpDeskTicketDetailsAsync(int ModuleId, Models.SyncfusionHelpDeskTicketDetails HelpDeskTicketDetail)
        {
            var result = await PostJsonAsync(CreateAuthorizationPolicyUrl($"{Apiurl}", ModuleId), HelpDeskTicketDetail);
            // ** TO DO - Return the updated SyncfusionHelpDeskTicketDetails
        }

        public async Task<Models.SyncfusionHelpDeskTickets> UpdateSyncfusionHelpDeskTicketsAsync(Models.SyncfusionHelpDeskTickets HelpDesk)
        {
            return await PutJsonAsync<Models.SyncfusionHelpDeskTickets>(CreateAuthorizationPolicyUrl($"{Apiurl}/{HelpDesk.HelpDeskTicketId}", HelpDesk.ModuleId), HelpDesk);
        }

        public async Task DeleteSyncfusionHelpDeskTicketsAsync(int HelpDeskId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{HelpDeskId}", ModuleId));
        }
    }
}
