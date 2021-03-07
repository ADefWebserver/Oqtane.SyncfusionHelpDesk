using System.Collections.Generic;
using Syncfusion.HelpDesk.Models;

namespace Syncfusion.HelpDesk.Repository
{
    public interface IHelpDeskRepository
    {
        IEnumerable<Models.HelpDesk> GetHelpDesks(int ModuleId);
        Models.HelpDesk GetHelpDesk(int HelpDeskId);
        Models.HelpDesk AddHelpDesk(Models.HelpDesk HelpDesk);
        Models.HelpDesk UpdateHelpDesk(Models.HelpDesk HelpDesk);
        void DeleteHelpDesk(int HelpDeskId);
    }
}
