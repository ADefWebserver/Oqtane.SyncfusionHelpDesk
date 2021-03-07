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

        public IEnumerable<Models.HelpDesk> GetHelpDesks(int ModuleId)
        {
            return _db.HelpDesk.Where(item => item.ModuleId == ModuleId);
        }

        public Models.HelpDesk GetHelpDesk(int HelpDeskId)
        {
            return _db.HelpDesk.Find(HelpDeskId);
        }

        public Models.HelpDesk AddHelpDesk(Models.HelpDesk HelpDesk)
        {
            _db.HelpDesk.Add(HelpDesk);
            _db.SaveChanges();
            return HelpDesk;
        }

        public Models.HelpDesk UpdateHelpDesk(Models.HelpDesk HelpDesk)
        {
            _db.Entry(HelpDesk).State = EntityState.Modified;
            _db.SaveChanges();
            return HelpDesk;
        }

        public void DeleteHelpDesk(int HelpDeskId)
        {
            Models.HelpDesk HelpDesk = _db.HelpDesk.Find(HelpDeskId);
            _db.HelpDesk.Remove(HelpDesk);
            _db.SaveChanges();
        }
    }
}
