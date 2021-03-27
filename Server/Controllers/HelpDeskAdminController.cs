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
using System.Linq;
using System.Threading.Tasks;
using Oqtane.Repository;
using System.Linq.Expressions;
using System;

namespace Syncfusion.HelpDesk.Controllers
{
    [Route(ControllerRoutes.Default)]
    public class HelpDeskAdminController : Controller
    {
        private readonly IHelpDeskRepository _HelpDeskRepository;
        private readonly IUserRepository _users;
        private readonly ILogManager _logger;
        protected int _entityId = -1;

        public HelpDeskAdminController(
            IHelpDeskRepository HelpDeskRepository, 
            IUserRepository users, 
            ILogManager logger, 
            IHttpContextAccessor accessor)
        {
            try
            {
                _HelpDeskRepository = HelpDeskRepository;
                _users = users;
                _logger = logger;

                if (accessor.HttpContext.Request.Query.ContainsKey("entityid"))
                {
                    _entityId = int.Parse(
                        accessor.HttpContext.Request.Query["entityid"]);
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
        public object Get(string entityid)
        {
            StringValues Skip;
            StringValues Take;
            StringValues OrderBy;

            // Filter the data
            var TotalRecordCount = 
                _HelpDeskRepository.GetSyncfusionHelpDeskTickets(
                int.Parse(entityid)).Count();

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
                    Items = 
                    _HelpDeskRepository.GetSyncfusionHelpDeskTickets(
                        int.Parse(entityid))
                    .OrderByDescending(orderby)
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
                    Items = _HelpDeskRepository.GetSyncfusionHelpDeskTickets(
                        int.Parse(entityid))
                    .OrderBy(orderby)
                    .Skip(skip)
                    .Take(top),
                    Count = TotalRecordCount
                };
            }
        }

        // Only an Administrator can call this method
        // GET: api/<controller>/1?entityid=z
        [HttpGet("{HelpDeskTicketId}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public SyncfusionHelpDeskTickets Get(
            string HelpDeskTicketId, 
            string entityid)
        {
            return _HelpDeskRepository.GetSyncfusionHelpDeskTicket
                (int.Parse(HelpDeskTicketId));
        }

        // Only an Administrator can update using this method
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.SyncfusionHelpDeskTickets Put(
            int id,
            [FromBody] Models.SyncfusionHelpDeskTickets updatedSyncfusionHelpDeskTickets)
        {
            if (ModelState.IsValid && 
                updatedSyncfusionHelpDeskTickets.ModuleId == _entityId)
            {
                updatedSyncfusionHelpDeskTickets = 
                    _HelpDeskRepository.UpdateSyncfusionHelpDeskTickets(
                        "Admin", 
                        updatedSyncfusionHelpDeskTickets);

                _logger.Log(LogLevel.Information, this, LogFunction.Update, 
                    "HelpDesk Updated {updatedSyncfusionHelpDeskTickets}",
                    updatedSyncfusionHelpDeskTickets);
            }

            return updatedSyncfusionHelpDeskTickets;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.SyncfusionHelpDeskTickets deletedSyncfusionHelpDeskTickets = 
                _HelpDeskRepository.GetSyncfusionHelpDeskTicket(id);
            if (deletedSyncfusionHelpDeskTickets != null && 
                deletedSyncfusionHelpDeskTickets.ModuleId == _entityId)
            {
                _HelpDeskRepository.DeleteSyncfusionHelpDeskTickets(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, 
                    "HelpDesk Deleted {HelpDeskId}", id);
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