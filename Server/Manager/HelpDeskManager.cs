using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Repository;
using Syncfusion.Helpdesk.Models;
using Syncfusion.Helpdesk.Repository;

namespace Syncfusion.Helpdesk.Manager
{
    public class HelpdeskManager : IInstallable, IPortable
    {
        private IHelpdeskRepository _HelpDeskRepository;
        private ISqlRepository _sql;

        public HelpdeskManager(
            IHelpdeskRepository HelpDeskRepository,
            ISqlRepository sql)
        {
            _HelpDeskRepository = HelpDeskRepository;
            _sql = sql;
        }

        public bool Install(Tenant tenant, string version)
        {
            return _sql.ExecuteScript(
                tenant,
                GetType().Assembly,
                "Syncfusion.Helpdesk." + version + ".sql");
        }

        public bool Uninstall(Tenant tenant)
        {
            return _sql.ExecuteScript(
                tenant,
                GetType().Assembly,
                "Syncfusion.Helpdesk.Uninstall.sql");
        }

        public string ExportModule(Module module)
        {
            string content = "";

            List<Models.SyncfusionHelpDeskTickets> HelpDesks =
                new List<SyncfusionHelpDeskTickets>();

            var AllTickets = _HelpDeskRepository
                .GetSyncfusionHelpDeskTickets(module.ModuleId).ToList();

            foreach (var Ticket in AllTickets)
            {
                var HelpDeskTicket = _HelpDeskRepository
                    .GetSyncfusionHelpDeskTicket(Ticket.HelpDeskTicketId);

                HelpDesks.Add(HelpDeskTicket);
            }

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
                    JsonSerializer
                    .Deserialize<List<Models.SyncfusionHelpDeskTickets>>(content);
            }
            if (HelpDesks != null)
            {
                foreach (var HelpDesk in HelpDesks)
                {
                    Models.SyncfusionHelpDeskTickets NewHelpDesk =
                        new SyncfusionHelpDeskTickets();

                    NewHelpDesk.ModuleId = module.ModuleId;
                    NewHelpDesk.TicketDate = HelpDesk.TicketDate;
                    NewHelpDesk.TicketStatus = HelpDesk.TicketStatus;
                    NewHelpDesk.TicketDescription = HelpDesk.TicketDescription;

                    NewHelpDesk.SyncfusionHelpDeskTicketDetails =
                        new List<SyncfusionHelpDeskTicketDetails>();

                    foreach (var TicketDetail in
                        HelpDesk.SyncfusionHelpDeskTicketDetails)
                    {
                        SyncfusionHelpDeskTicketDetails NewDetail =
                            new SyncfusionHelpDeskTicketDetails();

                        NewDetail.TicketDetailDate = TicketDetail.TicketDetailDate;
                        NewDetail.ModifiedBy = TicketDetail.ModifiedBy;
                        NewDetail.ModifiedOn = TicketDetail.ModifiedOn;
                        NewDetail.TicketDescription = TicketDetail.TicketDescription;

                        NewHelpDesk.SyncfusionHelpDeskTicketDetails.Add(NewDetail);
                    }

                    _HelpDeskRepository.AddSyncfusionHelpDeskTickets(NewHelpDesk);
                }
            }
        }

    }
}
