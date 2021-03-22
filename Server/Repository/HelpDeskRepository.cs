using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using Syncfusion.HelpDesk.Models;
using System;

namespace Syncfusion.HelpDesk.Repository
{
    public class HelpDeskRepository : IHelpDeskRepository, IService
    {
        private readonly HelpDeskContext _db;

        public HelpDeskRepository(HelpDeskContext context)
        {
            _db = context;
        }

        public IQueryable<Models.SyncfusionHelpDeskTickets> GetSyncfusionHelpDeskTickets(int ModuleId)
        {
            return _db.SyncfusionHelpDeskTickets.Where(item => item.ModuleId == ModuleId);
        }

        public Models.SyncfusionHelpDeskTickets GetSyncfusionHelpDeskTicket(int Id)
        {
            var HelpDeskTicket = _db.SyncfusionHelpDeskTickets
                .Where(item => item.HelpDeskTicketId == Id)
                .Include(x => x.SyncfusionHelpDeskTicketDetails).FirstOrDefault();

            // Strip out HelpDeskTicket from SyncfusionHelpDeskTicketDetails
            // to avoid trying to return self referencing object

            var FinalHelpDeskTicket = new SyncfusionHelpDeskTickets();

            FinalHelpDeskTicket.HelpDeskTicketId = HelpDeskTicket.HelpDeskTicketId;
            FinalHelpDeskTicket.ModuleId = HelpDeskTicket.ModuleId;
            FinalHelpDeskTicket.TicketDate = HelpDeskTicket.TicketDate;
            FinalHelpDeskTicket.TicketDescription = HelpDeskTicket.TicketDescription;
            FinalHelpDeskTicket.TicketStatus = HelpDeskTicket.TicketStatus;
            FinalHelpDeskTicket.CreatedBy = HelpDeskTicket.CreatedBy;
            FinalHelpDeskTicket.CreatedOn = HelpDeskTicket.CreatedOn;
            FinalHelpDeskTicket.ModifiedBy = HelpDeskTicket.ModifiedBy;
            FinalHelpDeskTicket.ModifiedOn = HelpDeskTicket.ModifiedOn;

            FinalHelpDeskTicket.SyncfusionHelpDeskTicketDetails = 
                new List<SyncfusionHelpDeskTicketDetails>();

            foreach (var item in HelpDeskTicket.SyncfusionHelpDeskTicketDetails)
            {
                item.HelpDeskTicket = null;
                FinalHelpDeskTicket.SyncfusionHelpDeskTicketDetails.Add(item);
            }

            return FinalHelpDeskTicket;
        }

        public Models.SyncfusionHelpDeskTickets AddSyncfusionHelpDeskTickets
            (Models.SyncfusionHelpDeskTickets SyncfusionHelpDeskTicket)
        {
                _db.SyncfusionHelpDeskTickets.Add(SyncfusionHelpDeskTicket);
                _db.SaveChanges();
                return SyncfusionHelpDeskTicket;
        }

        public Models.SyncfusionHelpDeskTickets UpdateSyncfusionHelpDeskTickets
            (string UpdateMode, Models.SyncfusionHelpDeskTickets UpdatedSyncfusionHelpDeskTickets)
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

                if (UpdatedSyncfusionHelpDeskTickets.SyncfusionHelpDeskTicketDetails != null)
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
                        }
                    }
                }

                _db.Entry(ExistingTicket).State = EntityState.Modified;
                _db.SaveChanges();
            }

            _db.SaveChanges();
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
