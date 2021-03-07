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

        public async Task<List<Models.HelpDesk>> GetHelpDesksAsync(int ModuleId)
        {
            List<Models.HelpDesk> HelpDesks = await GetJsonAsync<List<Models.HelpDesk>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", ModuleId));
            return HelpDesks.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.HelpDesk> GetHelpDeskAsync(int HelpDeskId, int ModuleId)
        {
            return await GetJsonAsync<Models.HelpDesk>(CreateAuthorizationPolicyUrl($"{Apiurl}/{HelpDeskId}", ModuleId));
        }

        public async Task<Models.HelpDesk> AddHelpDeskAsync(Models.HelpDesk HelpDesk)
        {
            return await PostJsonAsync<Models.HelpDesk>(CreateAuthorizationPolicyUrl($"{Apiurl}", HelpDesk.ModuleId), HelpDesk);
        }

        public async Task<Models.HelpDesk> UpdateHelpDeskAsync(Models.HelpDesk HelpDesk)
        {
            return await PutJsonAsync<Models.HelpDesk>(CreateAuthorizationPolicyUrl($"{Apiurl}/{HelpDesk.HelpDeskId}", HelpDesk.ModuleId), HelpDesk);
        }

        public async Task DeleteHelpDeskAsync(int HelpDeskId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{HelpDeskId}", ModuleId));
        }
    }
}
