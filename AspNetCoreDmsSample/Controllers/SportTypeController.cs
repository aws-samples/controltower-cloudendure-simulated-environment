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
    public class SportTypeController : BaseController
    {
        public SportTypeController(IDbContextService dbContextService) : base(dbContextService){}

        public async Task<IActionResult> Index(string searchString){
            ViewData["SearchString"] = searchString;
            return View();
        }

        // GET: SporType
        public async Task<IActionResult> SearchList(string sortOrder, string searchString, int? page)
        {

            var sportType = from sportTypes in _context.SportType select sportTypes;

            ViewData["SortOrder"] = sortOrder;
            ViewData["SearchString"] = searchString;

            if(!String.IsNullOrEmpty(searchString)){
                sportType = sportType.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString));
            }

            switch(sortOrder){
                case "name_desc":
                    sportType = sportType.OrderByDescending(s => s.Name);
                    break;
                case "name":
                    sportType = sportType.OrderBy(s => s.Name);
                    break;
                case "description_desc":
                    sportType = sportType.OrderByDescending(s => s.Description);
                    break;
                case "description":
                    sportType = sportType.OrderBy(s => s.Description);
                    break;
                default:
                    sportType = sportType.OrderBy(p => p.Name);
                    break;
            }

            int pageSize = 25;
            return PartialView("SearchList", await PaginatedList<SportType>.CreateAsync(sportType.AsNoTracking(), page ?? 1, pageSize));

        }

        // GET: SporType/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportType = await _context.SportType
                .SingleOrDefaultAsync(m => m.Name == id);
            if (sportType == null)
            {
                return NotFound();
            }

            return View(sportType);
        }

        // GET: SporType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SporType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] SportType sportType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sportType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sportType);
        }

        // GET: SporType/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportType = await _context.SportType.SingleOrDefaultAsync(m => m.Name == id);
            if (sportType == null)
            {
                return NotFound();
            }
            return View(sportType);
        }

        // POST: SporType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Description")] SportType sportType)
        {
            if (id != sportType.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sportType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportTypeExists(sportType.Name))
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
            return View(sportType);
        }

        // GET: SporType/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportType = await _context.SportType
                .SingleOrDefaultAsync(m => m.Name == id);
            if (sportType == null)
            {
                return NotFound();
            }

            return View(sportType);
        }

        // POST: SporType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sportType = await _context.SportType.SingleOrDefaultAsync(m => m.Name == id);
            _context.SportType.Remove(sportType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportTypeExists(string id)
        {
            return _context.SportType.Any(e => e.Name == id);
        }
    }
}
