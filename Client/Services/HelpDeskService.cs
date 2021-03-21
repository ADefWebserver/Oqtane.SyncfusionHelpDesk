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
        private string AdminApiurl => CreateApiUrl(_siteState.Alias, "HelpDeskAdmin");

        public async Task<List<Models.SyncfusionHelpDeskTickets>> GetSyncfusionHelpDeskTicketsByUserAsync(int ModuleId, string username)
        {
            return await GetJsonAsync<List<Models.SyncfusionHelpDeskTickets>>(CreateAuthorizationPolicyUrl($"{Apiurl}?username={username}", ModuleId));
        }

        public async Task<Models.SyncfusionHelpDeskTickets> GetSyncfusionHelpDeskTicketByUserAsync(int HelpDeskTicketId, int ModuleId, string username)
        {
            return await GetJsonAsync<Models.SyncfusionHelpDeskTickets>(CreateAuthorizationPolicyUrl($"{Apiurl}/{HelpDeskTicketId}?&username={username}", ModuleId));
        }

        public async Task<Models.SyncfusionHelpDeskTickets> AddSyncfusionHelpDeskTicketsAsync(Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTickets)
        {
            return await PostJsonAsync<Models.SyncfusionHelpDeskTickets>(CreateAuthorizationPolicyUrl($"{Apiurl}", SyncfusionHelpDeskTickets.ModuleId), SyncfusionHelpDeskTickets);
        }

        public async Task<Models.SyncfusionHelpDeskTickets> UpdateSyncfusionHelpDeskTicketsAsync(Models.SyncfusionHelpDeskTickets objSyncfusionHelpDeskTicket)
        {
            try
            {
                return await PostJsonAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{objSyncfusionHelpDeskTicket.HelpDeskTicketId}", objSyncfusionHelpDeskTicket.ModuleId), objSyncfusionHelpDeskTicket);
            }
            catch (System.Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        // Admin Methods

        public async Task<List<Models.SyncfusionHelpDeskTickets>> GetSyncfusionHelpDeskTicketsAdminAsync(int ModuleId)
        {
            return await GetJsonAsync<List<Models.SyncfusionHelpDeskTickets>>(CreateAuthorizationPolicyUrl($"{AdminApiurl}?moduleid={ModuleId}", ModuleId));
        }

        public async Task<Models.SyncfusionHelpDeskTickets> UpdateSyncfusionHelpDeskTicketsAdminAsync(Models.SyncfusionHelpDeskTickets objSyncfusionHelpDeskTicket)
        {
            return await PutJsonAsync<Models.SyncfusionHelpDeskTickets>(CreateAuthorizationPolicyUrl($"{AdminApiurl}/{objSyncfusionHelpDeskTicket.HelpDeskTicketId}", objSyncfusionHelpDeskTicket.ModuleId), objSyncfusionHelpDeskTicket);
        }

        public async Task DeleteSyncfusionHelpDeskTicketsAsync(int HelpDeskId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{AdminApiurl}/{HelpDeskId}", ModuleId));
        }
    }
}
