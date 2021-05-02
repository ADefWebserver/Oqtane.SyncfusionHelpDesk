using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using Syncfusion.Helpdesk.Models;
using System;

namespace Syncfusion.Helpdesk.Repository
{
    public class HelpdeskRepository : IHelpdeskRepository, IService
    {
        private readonly HelpdeskContext _db;

        public HelpdeskRepository(HelpdeskContext context)
        {
            _db = context;
        }

        public IQueryable<Models.SyncfusionHelpDeskTickets>
    GetSyncfusionHelpDeskTickets(int ModuleId)
        {
            return _db.SyncfusionHelpDeskTickets.Where(
                item => item.ModuleId == ModuleId);
        }

        public Models.SyncfusionHelpDeskTickets GetSyncfusionHelpDeskTicket(int Id)
        {
            var HelpDeskTicket = _db.SyncfusionHelpDeskTickets
                .Where(item => item.HelpDeskTicketId == Id)
                .Include(x => x.SyncfusionHelpDeskTicketDetails).FirstOrDefault();

            // Strip out HelpDeskTicket from SyncfusionHelpDeskTicketDetails
            // to avoid trying to return self referencing object

            var objDeskTicket = new SyncfusionHelpDeskTickets();

            objDeskTicket.HelpDeskTicketId = HelpDeskTicket.HelpDeskTicketId;
            objDeskTicket.ModuleId = HelpDeskTicket.ModuleId;
            objDeskTicket.TicketDate = HelpDeskTicket.TicketDate;
            objDeskTicket.TicketDescription = HelpDeskTicket.TicketDescription;
            objDeskTicket.TicketStatus = HelpDeskTicket.TicketStatus;
            objDeskTicket.CreatedBy = HelpDeskTicket.CreatedBy;
            objDeskTicket.CreatedOn = HelpDeskTicket.CreatedOn;
            objDeskTicket.ModifiedBy = HelpDeskTicket.ModifiedBy;
            objDeskTicket.ModifiedOn = HelpDeskTicket.ModifiedOn;

            objDeskTicket.SyncfusionHelpDeskTicketDetails =
                new List<SyncfusionHelpDeskTicketDetails>();

            foreach (var item in HelpDeskTicket.SyncfusionHelpDeskTicketDetails)
            {
                item.HelpDeskTicket = null;
                objDeskTicket.SyncfusionHelpDeskTicketDetails.Add(item);
            }

            return objDeskTicket;
        }

        public Models.SyncfusionHelpDeskTickets AddSyncfusionHelpDeskTickets
            (Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTicket)
        {
            _db.SyncfusionHelpDeskTickets.Add(SyncfusionHelpDeskTicket);
            _db.SaveChanges();
            return SyncfusionHelpDeskTicket;
        }

        public Models.SyncfusionHelpDeskTickets UpdateSyncfusionHelpDeskTickets(
            string UpdateMode,
            Models.SyncfusionHelpDeskTickets UpdatedSyncfusionHelpDeskTickets)
        {
            // Get the existing record
            var ExistingTicket =
                _db.SyncfusionHelpDeskTickets
                .Where(x => x.HelpDeskTicketId ==
                UpdatedSyncfusionHelpDeskTickets.HelpDeskTicketId)
                .FirstOrDefault();

            if (ExistingTicket != null)
            {
                if (UpdateMode == "Admin")
                {
                    // Only Admin can update these fields

                    ExistingTicket.TicketDate =
                        UpdatedSyncfusionHelpDeskTickets.TicketDate;

                    ExistingTicket.TicketDescription =
                        UpdatedSyncfusionHelpDeskTickets.TicketDescription;
                }

                ExistingTicket.TicketStatus =
                    UpdatedSyncfusionHelpDeskTickets.TicketStatus;

                // Insert any new TicketDetails

                if (UpdatedSyncfusionHelpDeskTickets.SyncfusionHelpDeskTicketDetails
                    != null)
                {
                    foreach (var item in
                        UpdatedSyncfusionHelpDeskTickets.SyncfusionHelpDeskTicketDetails)
                    {
                        if (item.HelpDeskTicketDetailId == 0)
                        {
                            // Create New HelpDeskTicketDetails record
                            SyncfusionHelpDeskTicketDetails newHelpDeskTicketDetails =
                                new SyncfusionHelpDeskTicketDetails();

                            newHelpDeskTicketDetails.HelpDeskTicketId =
                                UpdatedSyncfusionHelpDeskTickets.HelpDeskTicketId;

                            newHelpDeskTicketDetails.TicketDetailDate =
                                DateTime.Now;

                            newHelpDeskTicketDetails.TicketDescription =
                                item.TicketDescription;

                            _db.SyncfusionHelpDeskTicketDetails
                                .Add(newHelpDeskTicketDetails);

                            _db.SaveChanges();
                        }
                    }
                }

                _db.Entry(ExistingTicket).State = EntityState.Modified;
                _db.SaveChanges();
            }
           
            return ExistingTicket;
        }

        public void DeleteSyncfusionHelpDeskTickets(int Id)
        {
            Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTicket =
                _db.SyncfusionHelpDeskTickets.Find(Id);

            _db.SyncfusionHelpDeskTickets.Remove(SyncfusionHelpDeskTicket);
            _db.SaveChanges();
        }

    }
}
