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

        public async Task<List<Models.SyncfusionHelpDeskTickets>> GetSyncfusionHelpDeskTicketsAsync(int ModuleId)
        {
            List<Models.SyncfusionHelpDeskTickets> HelpDesks = await GetJsonAsync<List<Models.SyncfusionHelpDeskTickets>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", ModuleId));
            return HelpDesks.OrderBy(item => item.Id).ToList();
        }

        public async Task<Models.SyncfusionHelpDeskTickets> GetSyncfusionHelpDeskTicketsAsync(int HelpDeskId, int ModuleId)
        {
            return await GetJsonAsync<Models.SyncfusionHelpDeskTickets>(CreateAuthorizationPolicyUrl($"{Apiurl}/{HelpDeskId}", ModuleId));
        }

        public async Task<Models.SyncfusionHelpDeskTickets> AddSyncfusionHelpDeskTicketsAsync(Models.SyncfusionHelpDeskTickets HelpDesk)
        {
            return await PostJsonAsync<Models.SyncfusionHelpDeskTickets>(CreateAuthorizationPolicyUrl($"{Apiurl}", HelpDesk.ModuleId), HelpDesk);
        }

        public async Task<Models.SyncfusionHelpDeskTickets> UpdateSyncfusionHelpDeskTicketsAsync(Models.SyncfusionHelpDeskTickets HelpDesk)
        {
            return await PutJsonAsync<Models.SyncfusionHelpDeskTickets>(CreateAuthorizationPolicyUrl($"{Apiurl}/{HelpDesk.Id}", HelpDesk.ModuleId), HelpDesk);
        }

        public async Task DeleteSyncfusionHelpDeskTicketsAsync(int HelpDeskId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{HelpDeskId}", ModuleId));
        }
    }
}
