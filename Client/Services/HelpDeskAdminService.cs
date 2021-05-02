using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using System.Net.Http;
using System.Threading.Tasks;
using Syncfusion.Helpdesk.Models;

namespace Syncfusion.Helpdesk.Services
{
    public class HelpdeskAdminService :
            ServiceBase, IHelpdeskAdminService, IService
    {
        private readonly SiteState _siteState;

        public HelpdeskAdminService(
            HttpClient http,
            SiteState siteState) : base(http)
        {
            _siteState = siteState;
        }

        private string Apiurl => CreateApiUrl(
            _siteState.Alias, "HelpdeskAdmin");

        public async Task<SyncfusionHelpDeskTickets>
            GetSyncfusionHelpDeskTicketAdminAsync(
            int HelpDeskTicketId, int ModuleId)
        {
            return await GetJsonAsync<SyncfusionHelpDeskTickets>(
                CreateAuthorizationPolicyUrl(
                    $"{Apiurl}/{HelpDeskTicketId}",
                    ModuleId));
        }

        public async Task<SyncfusionHelpDeskTickets>
            UpdateSyncfusionHelpDeskTicketsAdminAsync(
            Models.SyncfusionHelpDeskTickets objSyncfusionHelpDeskTicket)
        {
            return await PutJsonAsync<SyncfusionHelpDeskTickets>(
                CreateAuthorizationPolicyUrl(
                    $"{Apiurl}/{objSyncfusionHelpDeskTicket.HelpDeskTicketId}",
                    objSyncfusionHelpDeskTicket.ModuleId),
                objSyncfusionHelpDeskTicket);
        }

        public async Task DeleteSyncfusionHelpDeskTicketsAsync(
            int HelpDeskId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl(
                $"{Apiurl}/{HelpDeskId}", ModuleId));
        }
    }
}