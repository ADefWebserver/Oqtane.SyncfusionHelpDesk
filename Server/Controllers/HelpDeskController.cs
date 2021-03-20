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
        
        // Only an Administrator can query all Tickets
        // GET: api/<controller>?moduleid=x
        //[HttpGet]
        //[Authorize(Policy = PolicyNames.EditModule)]
        //public IEnumerable<SyncfusionHelpDeskTickets> Get(string moduleid)
        //{
        //    return _HelpDeskRepository.GetSyncfusionHelpDeskTickets(int.Parse(moduleid))
        //        .OrderBy(x => x.HelpDeskTicketId)
        //        .ToList();
        //}

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

            return _HelpDeskRepository.GetSyncfusionHelpDeskTickets(int.Parse(entityid))
                .Where(x => x.CreatedBy == username)
                .OrderBy(x => x.HelpDeskTicketId)
                .ToList();
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
        // can call this method to add Ticket Details
        // POST api/<controller>/1
        [HttpPost("{Id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.SyncfusionHelpDeskTickets Post(int Id, [FromBody] Models.SyncfusionHelpDeskTicketDetails newSyncfusionHelpDeskTicketDetails)
        {
            SyncfusionHelpDeskTickets objSyncfusionHelpDeskTickets = new SyncfusionHelpDeskTickets();

            // Get User
            var User = _users.GetUser(this.User.Identity.Name);

            if (User != null)
            {
                // Get Ticket
                var ExistingTicket = _HelpDeskRepository.GetSyncfusionHelpDeskTicket(Id);

                // Ensure logged in user is the creator
                if(ExistingTicket.CreatedBy.ToLower() == User.Username.ToLower())
                {
                    objSyncfusionHelpDeskTickets = _HelpDeskRepository.AddHelpDeskTicketDetails(newSyncfusionHelpDeskTicketDetails);
                }                
            }

            return objSyncfusionHelpDeskTickets;
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