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
    public class SportLocationController : BaseController
    {
        public SportLocationController(IDbContextService dbContextService) : base(dbContextService){}

        public async Task<IActionResult> Index(string searchString){
            ViewData["SearchString"] = searchString;
            return View();
        }

        // GET: SportLocation
        public async Task<IActionResult> SearchList(string sortOrder, string searchString, int? page)
        {
            var sportLocation = from sportLocations in _context.SportLocation select sportLocations;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString;

            if(!String.IsNullOrEmpty(searchString)){
                sportLocation = sportLocation.Where(t => t.Name.Contains(searchString) || t.City.Contains(searchString));
            }

            switch(sortOrder){
                case "location_desc":
                    sportLocation = sportLocation.OrderByDescending(s => s.Name);
                    break;
                case "location":
                    sportLocation = sportLocation.OrderBy(s => s.Name);
                    break;
                case "city_desc":
                    sportLocation = sportLocation.OrderByDescending(s => s.City);
                    break;
                case "city":
                    sportLocation = sportLocation.OrderBy(s => s.City);
                    break;
                case "capacity_desc":
                    sportLocation = sportLocation.OrderByDescending(s => s.SeatingCapacity);
                    break;
                case "capacity":
                    sportLocation = sportLocation.OrderBy(s => s.SeatingCapacity);
                    break;
                case "levels_desc":
                    sportLocation = sportLocation.OrderByDescending(s => s.Levels);
                    break;
                case "levels":
                    sportLocation = sportLocation.OrderBy(s => s.Levels);
                    break;
                case "sections_desc":
                    sportLocation = sportLocation.OrderByDescending(s => s.Sections);
                    break;
                case "sections":
                    sportLocation = sportLocation.OrderBy(s => s.Sections);
                    break;
                default:
                    sportLocation = sportLocation.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            return PartialView("SearchList", await PaginatedList<SportLocation>.CreateAsync(sportLocation.AsNoTracking(), page ?? 1, pageSize));

        }

        // GET: SportLocation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportLocation = await _context.SportLocation
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sportLocation == null)
            {
                return NotFound();
            }

            return View(sportLocation);
        }

        // GET: SportLocation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SportLocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,City,SeatingCapacity,Levels,Sections")] SportLocation sportLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sportLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sportLocation);
        }

        // GET: SportLocation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportLocation = await _context.SportLocation.SingleOrDefaultAsync(m => m.Id == id);
            if (sportLocation == null)
            {
                return NotFound();
            }
            return View(sportLocation);
        }

        // POST: SportLocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,City,SeatingCapacity,Levels,Sections")] SportLocation sportLocation)
        {
            if (id != sportLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sportLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportLocationExists(sportLocation.Id))
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
            return View(sportLocation);
        }

        // GET: SportLocation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportLocation = await _context.SportLocation
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sportLocation == null)
            {
                return NotFound();
            }

            return View(sportLocation);
        }

        // POST: SportLocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sportLocation = await _context.SportLocation.SingleOrDefaultAsync(m => m.Id == id);
            _context.SportLocation.Remove(sportLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportLocationExists(int id)
        {
            return _context.SportLocation.Any(e => e.Id == id);
        }
    }
}
