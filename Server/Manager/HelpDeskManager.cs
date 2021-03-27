using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Repository;
using Syncfusion.HelpDesk.Models;
using Syncfusion.HelpDesk.Repository;

namespace Syncfusion.HelpDesk.Manager
{
    public class HelpDeskManager : IInstallable, IPortable
    {
        private IHelpDeskRepository _HelpDeskRepository;
        private ISqlRepository _sql;

        public HelpDeskManager(IHelpDeskRepository HelpDeskRepository, ISqlRepository sql)
        {
            _HelpDeskRepository = HelpDeskRepository;
            _sql = sql;
        }

        public bool Install(Tenant tenant, string version)
        {
            return _sql.ExecuteScript(tenant, GetType().Assembly, "Syncfusion.HelpDesk." + version + ".sql");
        }

        public bool Uninstall(Tenant tenant)
        {
            return _sql.ExecuteScript(tenant, GetType().Assembly, "Syncfusion.HelpDesk.Uninstall.sql");
        }

        public string ExportModule(Module module)
        {
            string content = "";
            List<Models.SyncfusionHelpDeskTickets> HelpDesks = 
                _HelpDeskRepository.GetSyncfusionHelpDeskTickets(module.ModuleId).ToList();

            if (HelpDesks != null)
            {
                content = JsonSerializer.Serialize(HelpDesks);
            }
            return content;
        }

        public void ImportModule(Module module, string content, string version)
        {
            List<Models.SyncfusionHelpDeskTickets> HelpDesks = null;
            if (!string.IsNullOrEmpty(content))
            {
                HelpDesks = 
                    JsonSerializer.Deserialize<List<Models.SyncfusionHelpDeskTickets>>(content);
            }
            if (HelpDesks != null)
            {
                foreach(var HelpDesk in HelpDesks)
                {
                    _HelpDeskRepository.AddSyncfusionHelpDeskTickets(
                        new Models.SyncfusionHelpDeskTickets {
                            HelpDeskTicketId = HelpDesk.HelpDeskTicketId,
                            ModuleId = module.ModuleId,
                            TicketDate = HelpDesk.TicketDate,
                            TicketStatus = HelpDesk.TicketStatus,
                            TicketDescription = HelpDesk.TicketDescription   
                        });
                }
            }
        }
    }
}