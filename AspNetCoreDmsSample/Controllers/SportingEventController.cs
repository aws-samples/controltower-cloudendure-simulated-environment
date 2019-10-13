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
    public class SportingEventController : BaseController
    {
        public SportingEventController(IDbContextService dbContextService) : base(dbContextService){}

        public IActionResult Index(string searchString){
            ViewData["SearchString"] = searchString;
            return View();
        }

        // GET: SportingEvent
        public IActionResult SearchList(string sortOrder, string searchString, int? page)
        {
            var sportingEvent = from sportingEvents in _context.SportingEvent.Include(s => s.AwayTeam).Include(s => s.HomeTeam).Include(s => s.Location).Include(s => s.SportTypeNameNavigation) select sportingEvents;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString;

            if(!String.IsNullOrEmpty(searchString)){
                sportingEvent = sportingEvent.Where(s => s.HomeTeam.Name.StartsWith(searchString) || s.AwayTeam.Name.StartsWith(searchString));
            }

            switch(sortOrder){
                case "start_date":
                    sportingEvent = sportingEvent.OrderBy(s => s.StartDate);
                    break;
                case "start_date_desc":
                    sportingEvent = sportingEvent.OrderByDescending(s => s.StartDate);
                    break;
                case "start_time":
                    sportingEvent = sportingEvent.OrderBy(s => s.StartDateTime);
                    break;
                case "start_time_desc":
                    sportingEvent = sportingEvent.OrderByDescending(s => s.StartDateTime);
                    break;
                case "sold_out":
                    sportingEvent = sportingEvent.OrderBy(s => s.SoldOut);
                    break;
                case "sold_out_desc":
                    sportingEvent = sportingEvent.OrderByDescending(s => s.SoldOut);
                    break;
                case "home_team":
                    sportingEvent = sportingEvent.OrderBy(s => s.HomeTeam.Name);
                    break;
                case "home_team_desc":
                    sportingEvent = sportingEvent.OrderByDescending(s => s.HomeTeam.Name);
                    break;
                case "away_team":
                    sportingEvent = sportingEvent.OrderBy(s => s.AwayTeam.Name);
                    break;
                case "away_team_desc":
                    sportingEvent = sportingEvent.OrderByDescending(s => s.AwayTeam.Name);
                    break;
                case "location":
                    sportingEvent = sportingEvent.OrderBy(s => s.Location.Name);
                    break;
                case "location_desc":
                    sportingEvent = sportingEvent.OrderByDescending(s => s.Location.Name);
                    break;
                case "sport_type":
                    sportingEvent = sportingEvent.OrderBy(s => s.SportTypeNameNavigation.Name);
                    break;
                case "sport_type_desc":
                    sportingEvent = sportingEvent.OrderByDescending(s => s.SportTypeNameNavigation.Name);
                    break;
                default:
                    sportingEvent = sportingEvent.OrderBy(s => s.StartDateTime);
                    break;
            }

            int pageSize = 10;
            return PartialView("SearchList", PaginatedList<SportingEvent>.CreateAsync(sportingEvent.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: SportingEvent/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportingEvent = await _context.SportingEvent
                .Include(s => s.AwayTeam)
                .Include(s => s.HomeTeam)
                .Include(s => s.Location)
                .Include(s => s.SportTypeNameNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sportingEvent == null)
            {
                return NotFound();
            }

            return View(sportingEvent);
        }

        // GET: SportingEvent/Create
        public IActionResult Create()
        {
            ViewData["AwayTeamId"] = new SelectList(_context.SportTeam, "Id", "Name");
            ViewData["HomeTeamId"] = new SelectList(_context.SportTeam, "Id", "Name");
            ViewData["LocationId"] = new SelectList(_context.SportLocation, "Id", "City");
            ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name");
            return View();
        }

        // POST: SportingEvent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SportTypeName,HomeTeamId,AwayTeamId,LocationId,StartDateTime,StartDate,SoldOut")] SportingEvent sportingEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sportingEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AwayTeamId"] = new SelectList(_context.SportTeam, "Id", "Name", sportingEvent.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.SportTeam, "Id", "Name", sportingEvent.HomeTeamId);
            ViewData["LocationId"] = new SelectList(_context.SportLocation, "Id", "City", sportingEvent.LocationId);
            ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name", sportingEvent.SportTypeName);
            return View(sportingEvent);
        }

        // GET: SportingEvent/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportingEvent = await _context.SportingEvent.SingleOrDefaultAsync(m => m.Id == id);
            if (sportingEvent == null)
            {
                return NotFound();
            }
            ViewData["AwayTeamId"] = new SelectList(_context.SportTeam, "Id", "Name", sportingEvent.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.SportTeam, "Id", "Name", sportingEvent.HomeTeamId);
            ViewData["LocationId"] = new SelectList(_context.SportLocation, "Id", "City", sportingEvent.LocationId);
            ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name", sportingEvent.SportTypeName);
            return View(sportingEvent);
        }

        // POST: SportingEvent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,SportTypeName,HomeTeamId,AwayTeamId,LocationId,StartDateTime,StartDate,SoldOut")] SportingEvent sportingEvent)
        {
            if (id != sportingEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sportingEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportingEventExists(sportingEvent.Id))
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
            ViewData["AwayTeamId"] = new SelectList(_context.SportTeam, "Id", "Name", sportingEvent.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.SportTeam, "Id", "Name", sportingEvent.HomeTeamId);
            ViewData["LocationId"] = new SelectList(_context.SportLocation, "Id", "City", sportingEvent.LocationId);
            ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name", sportingEvent.SportTypeName);
            return View(sportingEvent);
        }

        // GET: SportingEvent/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportingEvent = await _context.SportingEvent
                .Include(s => s.AwayTeam)
                .Include(s => s.HomeTeam)
                .Include(s => s.Location)
                .Include(s => s.SportTypeNameNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sportingEvent == null)
            {
                return NotFound();
            }

            return View(sportingEvent);
        }

        // POST: SportingEvent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var sportingEvent = await _context.SportingEvent.SingleOrDefaultAsync(m => m.Id == id);
            _context.SportingEvent.Remove(sportingEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportingEventExists(long id)
        {
            return _context.SportingEvent.Any(e => e.Id == id);
        }
    }
}
