using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using Syncfusion.HelpDesk.Models;

namespace Syncfusion.HelpDesk.Repository
{
    public class HelpDeskRepository : IHelpDeskRepository, IService
    {
        private readonly HelpDeskContext _db;

        public HelpDeskRepository(HelpDeskContext context)
        {
            _db = context;
        }

        public IEnumerable<Models.SyncfusionHelpDeskTickets> GetSyncfusionHelpDeskTickets(int ModuleId)
        {
            return _db.SyncfusionHelpDeskTickets.Where(item => item.ModuleId == ModuleId);
        }

        public Models.SyncfusionHelpDeskTickets GetSyncfusionHelpDeskTicket(int Id)
        {
            return _db.SyncfusionHelpDeskTickets.Find(Id);
        }

        public Models.SyncfusionHelpDeskTickets AddSyncfusionHelpDeskTickets(Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTicket)
        {
            _db.SyncfusionHelpDeskTickets.Add(SyncfusionHelpDeskTicket);
            _db.SaveChanges();
            return SyncfusionHelpDeskTicket;
        }

        public Models.SyncfusionHelpDeskTickets UpdateSyncfusionHelpDeskTickets(Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTicket)
        {
            _db.Entry(SyncfusionHelpDeskTicket).State = EntityState.Modified;
            _db.SaveChanges();
            return SyncfusionHelpDeskTicket;
        }

        public void DeleteSyncfusionHelpDeskTickets(int Id)
        {
            Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTicket = _db.SyncfusionHelpDeskTickets.Find(Id);
            _db.SyncfusionHelpDeskTickets.Remove(SyncfusionHelpDeskTicket);
            _db.SaveChanges();
        }
    }
}
