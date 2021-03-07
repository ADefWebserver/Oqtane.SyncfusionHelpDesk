using System.Collections.Generic;
using System.Threading.Tasks;
using Syncfusion.HelpDesk.Models;

namespace Syncfusion.HelpDesk.Services
{
    public interface IHelpDeskService 
    {
        Task<List<Models.HelpDesk>> GetHelpDesksAsync(int ModuleId);

        Task<Models.HelpDesk> GetHelpDeskAsync(int HelpDeskId, int ModuleId);

        Task<Models.HelpDesk> AddHelpDeskAsync(Models.HelpDesk HelpDesk);

        Task<Models.HelpDesk> UpdateHelpDeskAsync(Models.HelpDesk HelpDesk);

        Task DeleteHelpDeskAsync(int HelpDeskId, int ModuleId);
    }
}
