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
            _HelpDeskRepository = HelpDeskRepository;
            _users = users;
            _logger = logger;

            if (accessor.HttpContext.Request.Query.ContainsKey("entityid"))
            {
                _entityId = int.Parse(accessor.HttpContext.Request.Query["entityid"]);
            }
        }

        // Only an Administrator can query all Tickets
        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.EditModule)]
        public IEnumerable<SyncfusionHelpDeskTickets> Get(string moduleid)
        {
            return _HelpDeskRepository.GetSyncfusionHelpDeskTickets(int.Parse(moduleid))
                .OrderBy(x => x.HelpDeskTicketId)
                .ToList();
        }

        // Only an Administrator can update using this method
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.SyncfusionHelpDeskTickets Put(int id, [FromBody] Models.SyncfusionHelpDeskTickets updatedSyncfusionHelpDeskTickets)
        {
            if (ModelState.IsValid && updatedSyncfusionHelpDeskTickets.ModuleId == _entityId)
            {
                updatedSyncfusionHelpDeskTickets = _HelpDeskRepository.UpdateSyncfusionHelpDeskTickets(updatedSyncfusionHelpDeskTickets);

                _logger.Log(LogLevel.Information, this, LogFunction.Update, "HelpDesk Updated {updatedSyncfusionHelpDeskTickets}",
                    updatedSyncfusionHelpDeskTickets);
            }

            return updatedSyncfusionHelpDeskTickets;
        }
    }
}