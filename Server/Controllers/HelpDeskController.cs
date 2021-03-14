using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Syncfusion.HelpDesk.Models;
using Syncfusion.HelpDesk.Repository;
using System.Linq.Expressions;
using System;
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
        [HttpGet]
        [Authorize(Policy = PolicyNames.EditModule)]
        public object Get(string moduleid)
        {
            StringValues Skip;
            StringValues Take;
            StringValues OrderBy;

            // Filter the data
            var TotalRecordCount = _HelpDeskRepository.GetSyncfusionHelpDeskTickets(int.Parse(moduleid)).Count();

            int skip = (Request.Query.TryGetValue("$skip", out Skip))
                ? Convert.ToInt32(Skip[0]) : 0;

            int top = (Request.Query.TryGetValue("$top", out Take))
                ? Convert.ToInt32(Take[0]) : TotalRecordCount;

            string orderby =
                (Request.Query.TryGetValue("$orderby", out OrderBy))
                ? OrderBy.ToString() : "TicketDate";

            // Handle OrderBy direction
            if (orderby.EndsWith(" desc"))
            {
                orderby = orderby.Replace(" desc", "");

                return new
                {
                    Items = _HelpDeskRepository.GetSyncfusionHelpDeskTickets(int.Parse(moduleid))
                    .OrderBy(orderby)
                    .Skip(skip)
                    .Take(top),
                    Count = TotalRecordCount
                };
            }
            else
            {
                System.Reflection.PropertyInfo prop =
                    typeof(SyncfusionHelpDeskTickets).GetProperty(orderby);

                return new
                {
                    Items = _HelpDeskRepository.GetSyncfusionHelpDeskTickets(int.Parse(moduleid))
                    .OrderBy(orderby)
                    .Skip(skip)
                    .Take(top),
                    Count = TotalRecordCount
                };
            }
        }


        // A non-Administrator can only query their Tickets
        // GET: api/<controller>?moduleid=x&username=y
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public object Get(string moduleid, string username)
        {
            // Get User
            var User = _users.GetUser(this.User.Identity.Name);

            if (User == null)
            {
                return null;
            }

            StringValues Skip;
            StringValues Take;
            StringValues OrderBy;

            // Filter the data
            var TotalRecordCount = _HelpDeskRepository.GetSyncfusionHelpDeskTickets(int.Parse(moduleid)).Count();

            int skip = (Request.Query.TryGetValue("$skip", out Skip))
                ? Convert.ToInt32(Skip[0]) : 0;

            int top = (Request.Query.TryGetValue("$top", out Take))
                ? Convert.ToInt32(Take[0]) : TotalRecordCount;

            string orderby =
                (Request.Query.TryGetValue("$orderby", out OrderBy))
                ? OrderBy.ToString() : "TicketDate";

            // Handle OrderBy direction
            if (orderby.EndsWith(" desc"))
            {
                orderby = orderby.Replace(" desc", "");

                return new
                {
                    Items = _HelpDeskRepository.GetSyncfusionHelpDeskTickets(int.Parse(moduleid))
                    .Where(x => x.CreatedBy == User.Username)
                    .OrderBy(orderby)
                    .Skip(skip)
                    .Take(top),
                    Count = TotalRecordCount
                };
            }
            else
            {
                System.Reflection.PropertyInfo prop =
                    typeof(SyncfusionHelpDeskTickets).GetProperty(orderby);

                return new
                {
                    Items = _HelpDeskRepository.GetSyncfusionHelpDeskTickets(int.Parse(moduleid))
                    .Where(x => x.CreatedBy == User.Username)
                    .OrderBy(orderby)
                    .Skip(skip)
                    .Take(top),
                    Count = TotalRecordCount
                };
            }
        }

        // All users can Post
        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Task
            Post(SyncfusionHelpDeskTickets newSyncfusionHelpDeskTickets)
        {
            if (ModelState.IsValid && newSyncfusionHelpDeskTickets.ModuleId == _entityId)
            {
                // Add a new Help Desk Ticket
                newSyncfusionHelpDeskTickets = _HelpDeskRepository.AddSyncfusionHelpDeskTickets(newSyncfusionHelpDeskTickets);

                _logger.Log(LogLevel.Information, this, LogFunction.Create, 
                    "HelpDesk Added {newSyncfusionHelpDeskTickets}", newSyncfusionHelpDeskTickets);
            }            

            return Task.FromResult(newSyncfusionHelpDeskTickets);
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

    // From: https://bit.ly/30ypMCp
    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(
            this IQueryable<T> source, string propertyName)
        {
            return source.OrderBy(ToLambda<T>(propertyName));
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(
            this IQueryable<T> source, string propertyName)
        {
            return source.OrderByDescending(ToLambda<T>(propertyName));
        }

        private static Expression<Func<T, object>> ToLambda<T>(
            string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }
    }
}