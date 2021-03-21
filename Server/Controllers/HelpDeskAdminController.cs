using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Syncfusion.HelpDesk.Models;
using Syncfusion.HelpDesk.Repository;
using System.Linq;
using System.Threading.Tasks;
using Oqtane.Repository;

namespace Syncfusion.HelpDesk.Controllers
{
    [Route(ControllerRoutes.Default)]
    public class HelpDeskAdminController : Controller
    {
        private readonly IHelpDeskRepository _HelpDeskRepository;
        private readonly IUserRepository _users;
        private readonly ILogManager _logger;
        protected int _entityId = -1;

        public HelpDeskAdminController(IHelpDeskRepository HelpDeskRepository, IUserRepository users, ILogManager logger, IHttpContextAccessor accessor)
        {
            try
            {
                _HelpDeskRepository = HelpDeskRepository;
                _users = users;
                _logger = logger;

                if (accessor.HttpContext.Request.Query.ContainsKey("entityid"))
                {
                    _entityId = int.Parse(accessor.HttpContext.Request.Query["entityid"]);
                }
            }
            catch (System.Exception ex)
            {
                string error = ex.Message;
            }
        }

        // Only an Administrator can query all Tickets
        // GET: api/<controller>?entityid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.EditModule)]
        public IEnumerable<SyncfusionHelpDeskTickets> Get(string entityid)
        {
            return _HelpDeskRepository.GetSyncfusionHelpDeskTickets(int.Parse(entityid))
                .OrderBy(x => x.HelpDeskTicketId)
                .ToList();
        }

        // Only an Administrator can call this method
        // GET: api/<controller>/1?entityid=z
        [HttpGet("{HelpDeskTicketId}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public SyncfusionHelpDeskTickets Get(string HelpDeskTicketId, string entityid)
        {
            var HelpDeskTicket = _HelpDeskRepository.GetSyncfusionHelpDeskTicket(int.Parse(HelpDeskTicketId));

            // Strip out HelpDeskTicket from SyncfusionHelpDeskTicketDetails
            // to avoid trying to return self referencing object
            var FinalHelpDeskTicket = new SyncfusionHelpDeskTickets();
            FinalHelpDeskTicket.HelpDeskTicketId = HelpDeskTicket.HelpDeskTicketId;
            FinalHelpDeskTicket.ModuleId = HelpDeskTicket.ModuleId;
            FinalHelpDeskTicket.TicketDate = HelpDeskTicket.TicketDate;
            FinalHelpDeskTicket.TicketDescription = HelpDeskTicket.TicketDescription;
            FinalHelpDeskTicket.TicketStatus = HelpDeskTicket.TicketStatus;
            FinalHelpDeskTicket.SyncfusionHelpDeskTicketDetails = new List<SyncfusionHelpDeskTicketDetails>();

            foreach (var item in HelpDeskTicket.SyncfusionHelpDeskTicketDetails)
            {
                item.HelpDeskTicket = null;
                FinalHelpDeskTicket.SyncfusionHelpDeskTicketDetails.Add(item);
            }

            return FinalHelpDeskTicket;
        }

        // Only an Administrator can update using this method
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.SyncfusionHelpDeskTickets Put(int id, [FromBody] Models.SyncfusionHelpDeskTickets updatedSyncfusionHelpDeskTickets)
        {
            if (ModelState.IsValid && updatedSyncfusionHelpDeskTickets.ModuleId == _entityId)
            {
                updatedSyncfusionHelpDeskTickets = _HelpDeskRepository.UpdateSyncfusionHelpDeskTickets("Admin", updatedSyncfusionHelpDeskTickets);

                _logger.Log(LogLevel.Information, this, LogFunction.Update, "HelpDesk Updated {updatedSyncfusionHelpDeskTickets}",
                    updatedSyncfusionHelpDeskTickets);
            }

            return updatedSyncfusionHelpDeskTickets;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.SyncfusionHelpDeskTickets deletedSyncfusionHelpDeskTickets = _HelpDeskRepository.GetSyncfusionHelpDeskTicket(id);
            if (deletedSyncfusionHelpDeskTickets != null && deletedSyncfusionHelpDeskTickets.ModuleId == _entityId)
            {
                _HelpDeskRepository.DeleteSyncfusionHelpDeskTickets(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "HelpDesk Deleted {HelpDeskId}", id);
            }
        }
    }
}