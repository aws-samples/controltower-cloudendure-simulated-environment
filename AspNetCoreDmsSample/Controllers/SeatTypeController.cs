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
    public class SeatTypeController : BaseController
    {
        public SeatTypeController(IDbContextService dbContextService) : base(dbContextService){}

        public async Task<IActionResult> Index(string searchString){
            ViewData["SearchString"] = searchString;
            return View();
        }
        
        // GET: SeatType
        public async Task<IActionResult> SearchList(string sortOrder, string searchString, int? page)
        {
            var seatType = from seatTypes in _context.SeatType select seatTypes;
        
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString;

            if(!String.IsNullOrEmpty(searchString)){
                seatType = seatType.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString));
            }

            switch(sortOrder){
                case "name":
                    seatType = seatType.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    seatType = seatType.OrderByDescending(s => s.Name);
                    break;
                case "description":
                    seatType = seatType.OrderBy(s => s.Description);
                    break;
                case "description_desc":
                    seatType = seatType.OrderByDescending(s => s.Description);
                    break;
                case "relative_quality":
                    seatType = seatType.OrderBy(s => s.RelativeQuality);
                    break;
                case "relative_quality_desc":
                    seatType = seatType.OrderByDescending(s => s.RelativeQuality);
                    break;
                default:
                    seatType = seatType.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            return PartialView("SearchList", await PaginatedList<SeatType>.CreateAsync(seatType.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: SeatType/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seatType = await _context.SeatType
                .SingleOrDefaultAsync(m => m.Name == id);
            if (seatType == null)
            {
                return NotFound();
            }

            return View(seatType);
        }

        // GET: SeatType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SeatType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,RelativeQuality")] SeatType seatType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seatType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seatType);
        }

        // GET: SeatType/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seatType = await _context.SeatType.SingleOrDefaultAsync(m => m.Name == id);
            if (seatType == null)
            {
                return NotFound();
            }
            return View(seatType);
        }

        // POST: SeatType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Description,RelativeQuality")] SeatType seatType)
        {
            if (id != seatType.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seatType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeatTypeExists(seatType.Name))
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
            return View(seatType);
        }

        // GET: SeatType/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seatType = await _context.SeatType
                .SingleOrDefaultAsync(m => m.Name == id);
            if (seatType == null)
            {
                return NotFound();
            }

            return View(seatType);
        }

        // POST: SeatType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var seatType = await _context.SeatType.SingleOrDefaultAsync(m => m.Name == id);
            _context.SeatType.Remove(seatType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeatTypeExists(string id)
        {
            return _context.SeatType.Any(e => e.Name == id);
        }
    }
}
