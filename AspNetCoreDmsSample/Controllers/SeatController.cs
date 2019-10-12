using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DMSSample.Models;
using DMSSample.Services;

namespace DMSSample.Controllers
{
    public class SeatController : BaseController
    {
        public SeatController(IDbContextService dbContextService) : base(dbContextService){}

        public async Task<IActionResult> Index(string searchString){
            ViewData["SearchString"] = searchString;
            return View();
        }

        // GET: Seat
        public async Task<IActionResult> SearchList(string sortOrder, string searchString, int? page)
        {
            var seat = from seats in _context.Seat.Include(s => s.SeatTypeNavigation).Include(s => s.SportLocation) select seats;

            ViewData["SortOrder"] = sortOrder;
            ViewData["SearchString"] = searchString;

            if(!String.IsNullOrEmpty(searchString)){
                seat = seat.Where(s => s.SportLocation.City.StartsWith(searchString));
            }

            switch(sortOrder){
                case "sport_location":
                    seat = seat.OrderBy(s => s.SportLocation.City);
                    break;
                case "sport_location_desc":
                    seat = seat.OrderByDescending(s => s.SportLocation.City);
                    break;
                case "seat_level":
                    seat = seat.OrderBy(s => s.SeatLevel);
                    break;
                case "seat_level_desc":
                    seat = seat.OrderByDescending(s => s.SeatLevel);
                    break;
                case "seat_section":
                    seat = seat.OrderBy(s => s.SeatSection);
                    break;
                case "seat_section_desc":
                    seat = seat.OrderByDescending(s => s.SeatSection);
                    break;
                case "seat_row":
                    seat = seat.OrderBy(s => s.SeatRow);
                    break;
                case "seat_row_desc":
                    seat = seat.OrderByDescending(s => s.SeatRow);
                    break;
                case "seat":
                    seat = seat.OrderBy(s => s.Seat1);
                    break;
                case "seat_desc":
                    seat = seat.OrderByDescending(s => s.Seat1);
                    break;
                case "seat_type":
                    seat = seat.OrderBy(s => s.SeatType);
                    break;
                case "seat_type_desc":
                    seat = seat.OrderByDescending(s => s.SeatType);
                    break;
                default:
                    seat = seat.OrderBy(s => s.SportLocation.Name);
                    break;
            }

            int pageSize = 10;
            return PartialView("SearchList", await PaginatedList<Seat>.CreateAsync(seat.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Seat/Details/5
        public async Task<IActionResult> Details(int? sportLocationId, int? seatLevel, string seatSection, string seatRow, string seat1)
        {
            if (sportLocationId == null || seatLevel == null || String.IsNullOrEmpty(seatSection) || String.IsNullOrEmpty(seatRow) || String.IsNullOrEmpty(seat1))
            {
                return NotFound();
            }

             if (sportLocationId == null || seatLevel == null || String.IsNullOrEmpty(seatSection) || String.IsNullOrEmpty(seatRow) || String.IsNullOrEmpty(seat1))
            {
                return NotFound();
            }

            var seat = await _context.Seat.Include(s => s.SportLocation).SingleOrDefaultAsync(m => m.SportLocationId == sportLocationId && m.SeatLevel == seatLevel && m.SeatSection == seatSection && m.SeatRow == seatRow && m.Seat1 == seat1);
            if (seat == null)
            {
                return NotFound();
            }

            return View(seat);
        }

        // GET: Seat/Create
        public IActionResult Create()
        {
            ViewData["SeatType"] = new SelectList(_context.SeatType, "Name", "Name");
            ViewData["SportLocationId"] = new SelectList(_context.SportLocation, "Id", "City");
            return View();
        }

        // POST: Seat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SportLocationId,SeatLevel,SeatSection,SeatRow,Seat1,SeatType")] Seat seat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SeatType"] = new SelectList(_context.SeatType, "Name", "Name", seat.SeatType);
            ViewData["SportLocationId"] = new SelectList(_context.SportLocation, "Id", "City", seat.SportLocationId);
            return View(seat);
        }

        // GET: Seat/Edit/5
        public async Task<IActionResult> Edit(int? sportLocationId, int? seatLevel, string seatSection, string seatRow, string seat1)
        {
            if (sportLocationId  == null || seatLevel == null || String.IsNullOrEmpty(seatSection) || String.IsNullOrEmpty(seatRow) || String.IsNullOrEmpty(seat1))
            {
                return NotFound();
            }

            var seat = await _context.Seat.Include(s => s.SportLocation).SingleOrDefaultAsync(m => m.SportLocationId == sportLocationId && m.SeatLevel == seatLevel && m.SeatSection == seatSection && m.SeatRow == seatRow && m.Seat1 == seat1);
            if (seat == null)
            {
                return NotFound();
            }
            ViewData["SeatType"] = new SelectList(_context.SeatType, "Name", "Name", seat.SeatType);
            ViewData["SportLocationId"] = new SelectList(_context.SportLocation, "Id", "City", seat.SportLocationId);
            return View(seat);
        }

        // POST: Seat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("SportLocationId,SeatLevel,SeatSection,SeatRow,Seat1,SeatType")] Seat seat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeatExists(seat.SportLocationId, seat.SeatLevel, seat.SeatSection, seat.SeatRow, seat.Seat1))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SeatType"] = new SelectList(_context.SeatType, "Name", "Name", seat.SeatType);
            ViewData["SportLocationId"] = new SelectList(_context.SportLocation, "Id", "City", seat.SportLocationId);
            return View(seat);
        }

        // GET: Seat/Delete/5
        public async Task<IActionResult> Delete(int? sportLocationId, int? seatLevel, string seatSection, string seatRow, string seat1)
        {
            if (sportLocationId == null || seatLevel == null || String.IsNullOrEmpty(seatSection) || String.IsNullOrEmpty(seatRow) || String.IsNullOrEmpty(seat1))
            {
                return NotFound();
            }

            var seat = await _context.Seat.Include(s => s.SportLocation).SingleOrDefaultAsync(m => m.SportLocationId == sportLocationId && m.SeatLevel == seatLevel && m.SeatSection == seatSection && m.SeatRow == seatRow && m.Seat1 == seat1);
            if (seat == null)
            {
                return NotFound();
            }

            return View(seat);
        }

        // POST: Seat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? sportLocationId, int? seatLevel, string seatSection, string seatRow, string seat1)
        {
            if (sportLocationId == null || seatLevel == null || String.IsNullOrEmpty(seatSection) || String.IsNullOrEmpty(seatRow) || String.IsNullOrEmpty(seat1))
            {
                return NotFound();
            }

            var seat = await _context.Seat.SingleOrDefaultAsync(m => m.SportLocationId == sportLocationId && m.SeatLevel == seatLevel && m.SeatSection == seatSection && m.SeatRow == seatRow && m.Seat1 == seat1);
            _context.Seat.Remove(seat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeatExists(int? sportLocationId, int? seatLevel, string seatSection, string seatRow, string seat1)
        {
            return _context.Seat.Any(m => m.SportLocationId == sportLocationId && m.SeatLevel == seatLevel && m.SeatSection == seatSection && m.SeatRow == seatRow && m.Seat1 == seat1);
        }
    }
}
