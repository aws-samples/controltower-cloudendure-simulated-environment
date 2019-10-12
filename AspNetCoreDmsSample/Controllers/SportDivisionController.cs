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
    public class SportDivisionController : BaseController
    {
        public SportDivisionController(IDbContextService dbContextService) : base(dbContextService){}

        public async Task<IActionResult> Index(string searchString){
            ViewData["SearchString"] = searchString;
            return View();
        }
        // GET: SportDivision
        public async Task<IActionResult> SearchList(string sortOrder, string searchString, int? page)
        {
            var sportDivision = from sportDivisions in _context.SportDivision.Include(s => s.SportLeagueShortNameNavigation).Include(s => s.SportTypeNameNavigation) select sportDivisions;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString;

            if(!String.IsNullOrEmpty(searchString)){
                sportDivision = sportDivision.Where(s => s.LongName.Contains(searchString) || s.Description.Contains(searchString) || s.SportLeagueShortName.Equals(searchString));
            }

            switch(sortOrder){
                case "name":
                    sportDivision = sportDivision.OrderBy(s => s.LongName);
                    break;
                case "name_desc":
                    sportDivision = sportDivision.OrderByDescending(s => s.LongName);
                    break;
                case "description":
                    sportDivision = sportDivision.OrderBy(s => s.Description);
                    break;
                case "description_desc":
                    sportDivision = sportDivision.OrderByDescending(s => s.Description);
                    break;
                case "sport_league_short_name":
                    sportDivision = sportDivision.OrderBy(s => s.SportLeagueShortName);
                    break;
                case "sport_league_short_name_desc":
                    sportDivision = sportDivision.OrderByDescending(s => s.SportLeagueShortName);
                    break;
                case "sport_type":
                    sportDivision = sportDivision.OrderBy(s => s.SportTypeNameNavigation.Name);
                    break;
                case "sport_type_desc":
                    sportDivision = sportDivision.OrderByDescending(s => s.SportTypeNameNavigation.Name);
                    break;
                default:
                    sportDivision = sportDivision.OrderBy(s => s.LongName);
                    break;
            }

            int pageSize = 10;
            return PartialView("SearchList", await PaginatedList<SportDivision>.CreateAsync(sportDivision.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: SportDivision/Details/5
        public async Task<IActionResult> Details(String sportTypename, String sportLeagueShortName, String shortName)
        {
            if (String.IsNullOrEmpty(sportTypename) || string.IsNullOrEmpty(sportLeagueShortName) || string.IsNullOrEmpty(shortName))
            {
                return NotFound();
            }

            var sportDivision = await _context.SportDivision
                .Include(s => s.SportLeagueShortNameNavigation)
                .Include(s => s.SportTypeNameNavigation)
                .SingleOrDefaultAsync(m => m.SportTypeName == sportTypename && m.SportLeagueShortName == sportLeagueShortName && m.ShortName == shortName);
            
            if (sportDivision == null)
            {
                return NotFound();
            }

            return View(sportDivision);
        }

        // GET: SportDivision/Create
        public IActionResult Create()
        {
            ViewData["SportLeagueShortName"] = new SelectList(_context.SportLeague, "ShortName", "ShortName");
            ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name");
            return View();
        }

        // POST: SportDivision/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SportTypeName,SportLeagueShortName,ShortName,LongName,Description")] SportDivision sportDivision)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sportDivision);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SportLeagueShortName"] = new SelectList(_context.SportLeague, "ShortName", "ShortName", sportDivision.SportLeagueShortName);
            ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name", sportDivision.SportTypeName);
            return View(sportDivision);
        }

        // GET: SportDivision/Edit/5
        public async Task<IActionResult> Edit(String sportTypename, String sportLeagueShortName, String shortName)
        {
            if (String.IsNullOrEmpty(sportTypename) || string.IsNullOrEmpty(sportLeagueShortName) || string.IsNullOrEmpty(shortName))
            {
                return NotFound();
            }

            var sportDivision = await _context.SportDivision
                .Include(s => s.SportLeagueShortNameNavigation)
                .Include(s => s.SportTypeNameNavigation)
                .SingleOrDefaultAsync(m => m.SportTypeName == sportTypename && m.SportLeagueShortName == sportLeagueShortName && m.ShortName == shortName);
            
            if (sportDivision == null)
            {
                return NotFound();
            }

            //ViewData["SportLeagueShortName"] = new SelectList(_context.SportLeague, "ShortName", "ShortName", sportDivision.SportLeagueShortName);
            //ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name", sportDivision.SportTypeName);
            return View(sportDivision);
        }

        // POST: SportDivision/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("SportTypeName,SportLeagueShortName,ShortName,LongName,Description")] SportDivision sportDivision)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sportDivision);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportDivisionExists(sportDivision.SportTypeName))
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
            return View(sportDivision);
        }

        // GET: SportDivision/Delete/5
        public async Task<IActionResult> Delete(String sportTypename, String sportLeagueShortName, String shortName)
        {
            if (String.IsNullOrEmpty(sportTypename) || string.IsNullOrEmpty(sportLeagueShortName) || string.IsNullOrEmpty(shortName))
            {
                return NotFound();
            }

            var sportDivision = await _context.SportDivision
                .Include(s => s.SportLeagueShortNameNavigation)
                .Include(s => s.SportTypeNameNavigation)
                .SingleOrDefaultAsync(m => m.SportTypeName == sportTypename && m.SportLeagueShortName == sportLeagueShortName && m.ShortName == shortName);
            
            if (sportDivision == null)
            {
                return NotFound();
            }

            return View(sportDivision);
        }

        // POST: SportDivision/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(String sportTypename, String sportLeagueShortName, String shortName)
        {
            if (String.IsNullOrEmpty(sportTypename) || string.IsNullOrEmpty(sportLeagueShortName) || string.IsNullOrEmpty(shortName))
            {
                return NotFound();
            }

            var sportDivision = await _context.SportDivision
                .Include(s => s.SportLeagueShortNameNavigation)
                .Include(s => s.SportTypeNameNavigation)
                .SingleOrDefaultAsync(m => m.SportTypeName == sportTypename && m.SportLeagueShortName == sportLeagueShortName && m.ShortName == shortName);
            
            _context.SportDivision.Remove(sportDivision);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportDivisionExists(string id)
        {
            return _context.SportDivision.Any(e => e.SportTypeName == id);
        }
    }
}
