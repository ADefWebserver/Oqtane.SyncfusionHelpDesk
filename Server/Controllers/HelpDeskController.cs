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
    public class HelpDeskController : Controller
    {
        private readonly IHelpDeskRepository _HelpDeskRepository;
        private readonly IUserRepository _users;
        private readonly ILogManager _logger;
        protected int _entityId = -1;

        public HelpDeskController(IHelpDeskRepository HelpDeskRepository, IUserRepository users, ILogManager logger, IHttpContextAccessor accessor)
        {
            _HelpDeskRepository = HelpDeskRepository;
            _users = users;
            _logger = logger;

            if (accessor.HttpContext.Request.Query.ContainsKey("entityid"))
            {
                _entityId = int.Parse(accessor.HttpContext.Request.Query["entityid"]);
            }
        }

        // A non-Administrator can only query their Tickets
        // GET: api/<controller>?username=x&entityid=y
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<SyncfusionHelpDeskTickets> Get(string username, string entityid)
        {
            // Get User
            var User = _users.GetUser(this.User.Identity.Name);

            if (User.Username.ToLower() != username.ToLower())
            {
                return null;
            } 

            var HelpDeskTickets = _HelpDeskRepository.GetSyncfusionHelpDeskTickets(int.Parse(entityid))
                .Where(x => x.CreatedBy == username)
                .OrderBy(x => x.HelpDeskTicketId)
                .ToList();

            return HelpDeskTickets;
        }

        // A non-Administrator can only get a Ticket they created
        // GET: api/<controller>/1?username=y&entityid=z
        [HttpGet("{HelpDeskTicketId}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public SyncfusionHelpDeskTickets Get(string HelpDeskTicketId, string username, string entityid)
        {
            // Get User
            var User = _users.GetUser(this.User.Identity.Name);

            if (User.Username.ToLower() != username.ToLower())
            {
                return null;
            }

            var HelpDeskTicket = _HelpDeskRepository.GetSyncfusionHelpDeskTicket(int.Parse(HelpDeskTicketId));

            if(HelpDeskTicket.CreatedBy != User.Username)
            {
                return null;
            }

            // Strip out HelpDeskTicket from SyncfusionHelpDeskTicketDetails
            // to avoid trying to return self referencing object
            var FinalHelpDeskTicket = new SyncfusionHelpDeskTickets();
            FinalHelpDeskTicket.HelpDeskTicketId = HelpDeskTicket.HelpDeskTicketId;
            FinalHelpDeskTicket.ModuleId = HelpDeskTicket.HelpDeskTicketId;
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

        // All users can Post
        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Task Post([FromBody] Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTickets)
        {
            if (ModelState.IsValid && SyncfusionHelpDeskTickets.ModuleId == _entityId)
            {
                // Add a new Help Desk Ticket
                SyncfusionHelpDeskTickets = _HelpDeskRepository.AddSyncfusionHelpDeskTickets(SyncfusionHelpDeskTickets);

                _logger.Log(LogLevel.Information, this, LogFunction.Create, 
                    "HelpDesk Added {newSyncfusionHelpDeskTickets}", SyncfusionHelpDeskTickets);
            }            

            return Task.FromResult(SyncfusionHelpDeskTickets);
        }

        // Only users who created Ticket
        // can call this method to update Ticket
        // POST api/<controller>/1
        [HttpPost("{Id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public void Post(int Id, [FromBody] Models.SyncfusionHelpDeskTickets UpdateSyncfusionHelpDeskTicket)
        {
            // Get User
            var User = _users.GetUser(this.User.Identity.Name);

            if (User != null)
            {
                // Get Ticket
                var ExistingTicket = _HelpDeskRepository.GetSyncfusionHelpDeskTicket(Id);

                // Ensure logged in user is the creator
                if(ExistingTicket.CreatedBy.ToLower() == User.Username.ToLower())
                {
                    // Update Ticket 
                    _HelpDeskRepository.UpdateSyncfusionHelpDeskTickets("User", UpdateSyncfusionHelpDeskTicket);
                }                
            }
        }
    }
}