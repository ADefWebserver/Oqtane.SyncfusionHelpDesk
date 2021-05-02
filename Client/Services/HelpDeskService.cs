using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using Syncfusion.Helpdesk.Models;

namespace Syncfusion.Helpdesk.Services
{
    public class HelpdeskService :
        ServiceBase, IHelpdeskService, IService
    {
        private readonly SiteState _siteState;

        public HelpdeskService(
            HttpClient http, SiteState siteState) : base(http)
        {
            _siteState = siteState;
        }

        private string Apiurl => CreateApiUrl(_siteState.Alias, "Helpdesk");

        public async Task<List<SyncfusionHelpDeskTickets>>
            GetSyncfusionHelpDeskTicketsByUserAsync(
            int ModuleId, string username)
        {
            return await GetJsonAsync<List<SyncfusionHelpDeskTickets>>(
                CreateAuthorizationPolicyUrl(
                    $"{Apiurl}?username={username}", ModuleId));
        }

        public async Task<SyncfusionHelpDeskTickets>
            GetSyncfusionHelpDeskTicketByUserAsync(
            int HelpDeskTicketId, int ModuleId, string username)
        {
            return await GetJsonAsync<SyncfusionHelpDeskTickets>(
                CreateAuthorizationPolicyUrl(
                    $"{Apiurl}/{HelpDeskTicketId}?&username={username}", ModuleId));
        }

        public async Task<SyncfusionHelpDeskTickets>
            AddSyncfusionHelpDeskTicketsAsync(
            SyncfusionHelpDeskTickets SyncfusionHelpDeskTickets)
        {
            return await PostJsonAsync<SyncfusionHelpDeskTickets>(
                CreateAuthorizationPolicyUrl($"{Apiurl}",
                SyncfusionHelpDeskTickets.ModuleId),
                SyncfusionHelpDeskTickets);
        }

        public async Task<SyncfusionHelpDeskTickets>
            UpdateSyncfusionHelpDeskTicketsAsync(
            SyncfusionHelpDeskTickets objSyncfusionHelpDeskTicket)
        {
            return await PostJsonAsync(
                CreateAuthorizationPolicyUrl(
                    $"{Apiurl}/{objSyncfusionHelpDeskTicket.HelpDeskTicketId}",
                    objSyncfusionHelpDeskTicket.ModuleId),
                objSyncfusionHelpDeskTicket);
        }
    }
}
