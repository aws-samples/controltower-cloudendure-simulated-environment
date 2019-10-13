using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DMSSample.Models;
using DMSSample.Services;
using Newtonsoft.Json;

namespace DMSSample.Controllers
{
    public class SportingEventTicketController : BaseController
    {
        public SportingEventTicketController(IDbContextService dbContextService) : base(dbContextService){}

        public IActionResult Index(string searchString, string sportingEventDateFilter, int? sportingEventIdFilter){
            SportingEventTicketFilter model = new SportingEventTicketFilter();
            if(!string.IsNullOrEmpty(sportingEventDateFilter)){
                model.SportingEventDateFilter = DateTime.Parse(sportingEventDateFilter);
            }else{
                model.SportingEventDateFilter = DateTime.Today;
            }
            model.SportingEventIdFilter = sportingEventIdFilter;

            ViewData["SearchString"] = searchString;
            return View(model);
        }

        public JsonResult FilterList(String sportingEventDate){
            if(sportingEventDate != null){
                var sportingEvent = from sportingEvents in _context.SportingEvent.Include(ht => ht.HomeTeam).Include(at => at.AwayTeam) select sportingEvents;
                var sportingEventDateObj = DateTime.Parse(sportingEventDate);
                sportingEvent = sportingEvent.Where(se => 
                                                        se.StartDate.Value.Day ==  sportingEventDateObj.Day && 
                                                        se.StartDate.Value.Month == sportingEventDateObj.Month &&
                                                        se.StartDate.Value.Year == sportingEventDateObj.Year    
                                                    );
                sportingEvent = sportingEvent.OrderByDescending(se => se.StartDateTime);
                var sportingEventList = sportingEvent.ToListAsync();
                return Json(new SelectList(sportingEventList.Result, "Id", "EventDescription"));
            }
            return Json(string.Empty);
        }

        public JsonResult SportLocationDetails(int? sportingEventId){
            if(sportingEventId != null){
                var sportingEvent = from sportingEvents in _context.SportingEvent.Where(se => se.Id == sportingEventId).Include(sl => sl.Location).Include(sl => sl.Location).Include(s => s.Location.Seat) select sportingEvents;
                var sportLocationItem = sportingEvent.FirstOrDefaultAsync();
                
                SportLocation sportLocation = sportLocationItem.Result.Location;
                var locationData =  new { id = sportLocation.Id, city = sportLocation.City, name = sportLocation.Name };

                var seatLevels = from levels in sportLocation.Seat.GroupBy(s => s.SeatLevel) select new { level = levels.Key };
                var seatLevelsData = seatLevels.ToList();

                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
                jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                jsonSerializerSettings.MaxDepth = 1;
                jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                var returnObject = new {location = locationData, seatLevels = seatLevelsData };

                return Json(returnObject, jsonSerializerSettings);
            }
            return Json(String.Empty);
        }

        public JsonResult SeatSectionList(int? sportLocationId, int? seatLevel){
            if(sportLocationId != null && seatLevel != null){
                var seatSections = from sections in _context.Seat.Where(s => s.SportLocationId == sportLocationId && s.SeatLevel == seatLevel).GroupBy(s => s.SeatSection) select new { section = sections.Key };
                var seatSectionsData = seatSections.ToListAsync();

                return Json(seatSectionsData.Result);
            }
            return Json(String.Empty);
        }

        public JsonResult SeatRowList(int? sportLocationId, int? seatLevel, string seatSection){
            if(sportLocationId != null && seatLevel != null && !string.IsNullOrEmpty(seatSection)){
                var seatRows = from rows in _context.Seat.Where(s => s.SportLocationId == sportLocationId && s.SeatLevel == seatLevel && s.SeatSection == seatSection).GroupBy(s => s.SeatRow) select new { row = rows.Key };
                var seatRowsData = seatRows.ToListAsync();

                return Json(seatRowsData.Result);
            }
            return Json(String.Empty);
        }
        public JsonResult SeatList(int? sportLocationId, int? seatLevel, string seatSection, string seatRow){
            if(sportLocationId != null && seatLevel != null && !string.IsNullOrEmpty(seatSection) && !string.IsNullOrEmpty(seatRow)){
                var seatItems = from seats in _context.Seat.Where(s => s.SportLocationId == sportLocationId && s.SeatLevel == seatLevel && s.SeatSection == seatSection && s.SeatRow == seatRow).GroupBy(s => s.Seat1) select new { seat = seats.Key };
                var seatsData = seatItems.ToListAsync();

                return Json(seatsData.Result);
            }
            return Json(String.Empty);
        }

        // GET: SportingEventTicket
        public async Task<IActionResult> SearchList(string sortOrder, string searchString, int? page, int? sportingEventId)
        {
            var ticket = from tickets in _context.SportingEventTicket.Where(s => s.SportingEventId == sportingEventId).Include(s => s.S).Include(s => s.SportingEvent).Include(s => s.Ticketholder) select tickets;    
        
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString;
            
            if(!String.IsNullOrEmpty(searchString)){
                ticket = ticket.Where(t => t.Ticketholder.FullName.StartsWith(searchString));
            }

            switch(sortOrder){
                case "ticket_price_desc":
                    ticket = ticket.OrderByDescending(t => t.TicketPrice);
                    break;
                case "ticket_price":
                    ticket = ticket.OrderBy(t => t.TicketPrice);
                    break;
                case "seat_section_desc":
                    ticket = ticket.OrderByDescending(t => t.S.SeatSection);
                    break;
                case "seat_section":
                    ticket = ticket.OrderBy(t => t.S.SeatSection);
                    break;
                case "sport_type_desc":
                    ticket = ticket.OrderByDescending(t => t.SportingEvent.SportTypeName);
                    break;
                case "sport_type":
                    ticket = ticket.OrderBy(t => t.SportingEvent.SportTypeName);
                    break;
                case "ticket_holder_desc":
                    ticket = ticket.OrderByDescending(t => t.Ticketholder.FullName);
                    break;
                case "ticket_holder":
                    ticket = ticket.OrderBy(t => t.Ticketholder.FullName);
                    break;
                default:
                    ticket = ticket.OrderBy(t => t.TicketPrice);
                    break;
            }

            int pageSize = 10;
            return PartialView("SearchList", await PaginatedList<SportingEventTicket>.CreateAsync(ticket.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: SportingEventTicket/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportingEventTicket = await _context.SportingEventTicket
                .Include(s => s.S)
                .Include(s => s.SportingEvent)
                .Include(s => s.Ticketholder)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sportingEventTicket == null)
            {
                return NotFound();
            }

            return View(sportingEventTicket);
        }

        // GET: SportingEventTicket/Create
        public IActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: SportingEventTicket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind("Id,SportingEventId,SportLocationId,SeatLevel,SeatSection,SeatRow,Seat,TicketholderId,TicketPrice")] SportingEventTicket sportingEventTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sportingEventTicket);
                _context.SaveChangesAsync();
                OperationResult result = OperationResult.Succeeded("Ticket Created Successfully!");
                return Json(result);
            }else{
                return BuildModelStateJson(ModelState);
            }
        }

        // GET: SportingEventTicket/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportingEventTicket = await _context.SportingEventTicket
                .Include(s => s.SportingEvent)
                .Include(ht => ht.SportingEvent.HomeTeam)
                .Include(at => at.SportingEvent.AwayTeam)
                .Include(l => l.SportingEvent.Location)
                .Include(s => s.Ticketholder)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sportingEventTicket == null)
            {
                return NotFound();
            }

            return PartialView("Edit", sportingEventTicket);
        }

        // POST: SportingEventTicket/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id,SportingEventId,SportLocationId,SeatLevel,SeatSection,SeatRow,Seat,TicketholderId,TicketPrice")] SportingEventTicket sportingEventTicket)
        {
            if (id != sportingEventTicket.Id)
            {
                return NotFound();
            }

             if (ModelState.IsValid)
            {
                _context.Update(sportingEventTicket);
                _context.SaveChangesAsync();
                OperationResult result = OperationResult.Succeeded("Ticket Updated Successfully!");
                return Json(result);
            }else{
                return BuildModelStateJson(ModelState);
            }
        }

        // GET: SportingEventTicket/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportingEventTicket = await _context.SportingEventTicket
                .Include(s => s.S)
                .Include(s => s.SportingEvent)
                .Include(s => s.Ticketholder)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sportingEventTicket == null)
            {
                return NotFound();
            }

            return View(sportingEventTicket);
        }

        // POST: SportingEventTicket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var sportingEventTicket = await _context.SportingEventTicket.SingleOrDefaultAsync(m => m.Id == id);
            _context.SportingEventTicket.Remove(sportingEventTicket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportingEventTicketExists(long id)
        {
            return _context.SportingEventTicket.Any(e => e.Id == id);
        }
    }
}
