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
    public class SportTeamController : BaseController
    {
        public SportTeamController(IDbContextService dbContextService) : base(dbContextService){}

        public async Task<IActionResult> Index(string searchString){
            ViewData["SearchString"] = searchString;
            return View();
        }

        // GET: SportTeam
        public async Task<IActionResult> SearchList(string sortOrder, string searchString, int? page)
        {
            var sportTeam = from sportTeams in _context.SportTeam.Include(s => s.HomeField).Include(s => s.SportTypeNameNavigation) select sportTeams;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString;

            if(!String.IsNullOrEmpty(searchString)){
                sportTeam = sportTeam.Where(s => s.Name.StartsWith(searchString) || s.AbbreviatedName.StartsWith(searchString));
            }

            switch(sortOrder){
                case "name_desc":
                    sportTeam = sportTeam.OrderByDescending(p => p.Name);
                    break;
                case "name":
                    sportTeam = sportTeam.OrderBy(p => p.Name);
                    break;
                case "abbreviated_name_desc":
                    sportTeam = sportTeam.OrderByDescending(p => p.AbbreviatedName);
                    break;
                case "abbreviated_name":
                    sportTeam = sportTeam.OrderBy(p => p.AbbreviatedName);
                    break;
                case "sport_league_short_name_desc":
                    sportTeam = sportTeam.OrderByDescending(p => p.SportLeagueShortName);
                    break;
                case "sport_league_short_name":
                    sportTeam = sportTeam.OrderBy(p => p.SportLeagueShortName);
                    break;
                case "sport_division_short_name_desc":
                    sportTeam = sportTeam.OrderByDescending(p => p.SportDivisionShortName);
                    break;
                case "sport_division_short_name":
                    sportTeam = sportTeam.OrderBy(p => p.SportDivisionShortName);
                    break;
                case "home_field_desc":
                    sportTeam = sportTeam.OrderByDescending(p => p.HomeField.City);
                    break;
                case "home_field":
                    sportTeam = sportTeam.OrderBy(p => p.HomeField.City);
                    break;
                case "sport_type_desc":
                    sportTeam = sportTeam.OrderByDescending(p => p.SportTypeName);
                    break;
                case "sport_type":
                    sportTeam = sportTeam.OrderBy(s => s.SportTypeName);
                    break;
                default:
                    sportTeam = sportTeam.OrderBy(p => p.Name);
                    break;
            }

            int pageSize = 10;
            return PartialView("SearchList", await PaginatedList<SportTeam>.CreateAsync(sportTeam.AsNoTracking(), page ?? 1, pageSize));

        }

        // GET: SportTeam/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportTeam = await _context.SportTeam
                .Include(s => s.HomeField)
                .Include(s => s.SportTypeNameNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sportTeam == null)
            {
                return NotFound();
            }

            return View(sportTeam);
        }

        // GET: SportTeam/Create
        public IActionResult Create()
        {
            ViewData["HomeFieldId"] = new SelectList(_context.SportLocation, "Id", "City");
            ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name");
            return View();
        }

        // POST: SportTeam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AbbreviatedName,HomeFieldId,SportTypeName,SportLeagueShortName,SportDivisionShortName")] SportTeam sportTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sportTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HomeFieldId"] = new SelectList(_context.SportLocation, "Id", "City", sportTeam.HomeFieldId);
            ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name", sportTeam.SportTypeName);
            return View(sportTeam);
        }

        // GET: SportTeam/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportTeam = await _context.SportTeam.SingleOrDefaultAsync(m => m.Id == id);
            if (sportTeam == null)
            {
                return NotFound();
            }
            ViewData["HomeFieldId"] = new SelectList(_context.SportLocation, "Id", "City", sportTeam.HomeFieldId);
            ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name", sportTeam.SportTypeName);
            return View(sportTeam);
        }

        // POST: SportTeam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AbbreviatedName,HomeFieldId,SportTypeName,SportLeagueShortName,SportDivisionShortName")] SportTeam sportTeam)
        {
            if (id != sportTeam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sportTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportTeamExists(sportTeam.Id))
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
            ViewData["HomeFieldId"] = new SelectList(_context.SportLocation, "Id", "City", sportTeam.HomeFieldId);
            ViewData["SportTypeName"] = new SelectList(_context.SportType, "Name", "Name", sportTeam.SportTypeName);
            return View(sportTeam);
        }

        // GET: SportTeam/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportTeam = await _context.SportTeam
                .Include(s => s.HomeField)
                .Include(s => s.SportTypeNameNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sportTeam == null)
            {
                return NotFound();
            }

            return View(sportTeam);
        }

        // POST: SportTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sportTeam = await _context.SportTeam.SingleOrDefaultAsync(m => m.Id == id);
            _context.SportTeam.Remove(sportTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportTeamExists(int id)
        {
            return _context.SportTeam.Any(e => e.Id == id);
        }
    }
}
