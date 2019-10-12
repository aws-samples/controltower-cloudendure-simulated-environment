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
    public class NameDataController : BaseController
    {
        public NameDataController(IDbContextService dbContextService) : base(dbContextService){
        }

        public async Task<IActionResult> Index(string searchString){
            ViewData["SearchString"] = searchString;
            return View();
        }

        // GET: NameData
        public async Task<IActionResult> SearchList(string sortOrder, string searchString, int? page)
        {
            var nameData = from p in _context.NameData select p;

            ViewData["SortOrder"] = sortOrder;
            ViewData["SearchString"] = searchString;

            if(!String.IsNullOrEmpty(searchString)){
                nameData = nameData.Where(p => p.Name.StartsWith(searchString));
            }

            switch(sortOrder){
                case "name_desc":
                    nameData = nameData.OrderByDescending(p => p.Name);
                    break;
                case "name":
                    nameData = nameData.OrderBy(p => p.Name);
                    break;
                case "name_type_desc":
                    nameData = nameData.OrderByDescending(p => p.NameType);
                    break;
                case "name_type":
                    nameData = nameData.OrderBy(p => p.NameType);
                    break;
                default:
                    nameData = nameData.OrderBy(p => p.NameType);
                    break;
            }

            int pageSize = 10;
            return PartialView("SearchList", await PaginatedList<NameData>.CreateAsync(nameData.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: NameData/Details/5
        public async Task<IActionResult> Details(string nameType, string name)
        {
            if (nameType == null || name == null)
            {
                return NotFound();
            }

            var nameData = await _context.NameData
                .SingleOrDefaultAsync(m => m.NameType == nameType && m.Name == name);
            if (nameData == null)
            {
                return NotFound();
            }

            return View(nameData);
        }

        // GET: NameData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NameData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NameType,Name")] NameData nameData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nameData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nameData);
        }

        // GET: NameData/Delete/5
        public async Task<IActionResult> Delete(string nameType, string name)
        {
            if (nameType == null || name == null)
            {
                return NotFound();
            }

            var nameData = await _context.NameData
                .SingleOrDefaultAsync(m => m.NameType == nameType && m.Name == name);
            if (nameData == null)
            {
                return NotFound();
            }

            return View(nameData);
        }

        // POST: NameData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string nameType, string name)
        {
            var nameData = await _context.NameData.SingleOrDefaultAsync(m => m.NameType == nameType && m.Name == name);
            _context.NameData.Remove(nameData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NameDataExists(string id)
        {
            return _context.NameData.Any(e => e.NameType == id);
        }
    }
}
