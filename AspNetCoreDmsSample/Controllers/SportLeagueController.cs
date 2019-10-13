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
    public class SportLeagueController : BaseController
    {
        public SportLeagueController(IDbContextService dbContextService) : base(dbContextService){}

        public IActionResult Index(string searchString){
            ViewData["SearchString"] = searchString;
            return View();
        }
        // GET: SportLeague
        public async Task<IActionResult> SearchList(string sortOrder, string searchString, int? page)
        {
            var sportLeague = from sportLeagues in _context.SportLeague.Include(s => s.SportTypeNameNavigation) select sportLeagues;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString;

            if(!String.IsNullOrEmpty(searchString)){
                sportLeague = sportLeague.Where(t => t.LongName.Contains(searchString) || t.Description.Contains(searchString));
            }

            switch(sortOrder){
                case "league_name_desc":
                    sportLeague = sportLeague.OrderByDescending(t => t.LongName);
                    break;
                case "league_name":
                    sportLeague = sportLeague.OrderBy(t => t.LongName);
                    break;
                case "description_desc":
                    sportLeague = sportLeague.OrderByDescending(t => t.Description);
                    break;
                case "description":
                    sportLeague = sportLeague.OrderBy(t => t.Description);
                    break;
                case "sport_type_desc":
                    sportLeague = sportLeague.OrderByDescending(t => t.SportTypeNameNavigation.Name);
                    break;
                case "sport_type":
                    sportLeague = sportLeague.OrderBy(t => t.SportTypeNameNavigation.Name);
                    break;
                default:
                    sportLeague = sportLeague.OrderBy(t => t.LongName);
                    break;
            }

            int pageSize = 10;
            return PartialView("SearchList", await PaginatedList<SportLeague>.CreateAsync(sportLeague.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: SportLeague/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportLeague = await _context.SportLeague
                .Include(s => s.SportTypeNameNavigation)
                .SingleOrDefaultAsync(m => m.ShortName == id);
            if (sportLeague == null)
            {
                return NotFound();
            }

            return View(sportLeague);
        }

        // GET: SportLeague/Create
        public IActionResult Create()
        {
            ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name");
            return View();
        }

        // POST: SportLeague/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SportTypeName,ShortName,LongName,Description")] SportLeague sportLeague)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sportLeague);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name", sportLeague.SportTypeName);
            return View(sportLeague);
        }

        // GET: SportLeague/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportLeague = await _context.SportLeague.SingleOrDefaultAsync(m => m.ShortName == id);
            if (sportLeague == null)
            {
                return NotFound();
            }
            ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name", sportLeague.SportTypeName);
            return View(sportLeague);
        }

        // POST: SportLeague/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SportTypeName,ShortName,LongName,Description")] SportLeague sportLeague)
        {
            if (id != sportLeague.ShortName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sportLeague);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportLeagueExists(sportLeague.ShortName))
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
            ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name", sportLeague.SportTypeName);
            return View(sportLeague);
        }

        // GET: SportLeague/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportLeague = await _context.SportLeague
                .Include(s => s.SportTypeNameNavigation)
                .SingleOrDefaultAsync(m => m.ShortName == id);
            if (sportLeague == null)
            {
                return NotFound();
            }

            return View(sportLeague);
        }

        // POST: SportLeague/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sportLeague = await _context.SportLeague.SingleOrDefaultAsync(m => m.ShortName == id);
            _context.SportLeague.Remove(sportLeague);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportLeagueExists(string id)
        {
            return _context.SportLeague.Any(e => e.ShortName == id);
        }
    }
}
