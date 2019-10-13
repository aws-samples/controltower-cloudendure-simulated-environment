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
    public class PlayerController : BaseController
    {
        public PlayerController(IDbContextService dbContextService) : base(dbContextService){}

        public IActionResult Index(string searchString){
            ViewData["SearchString"] = searchString;
            return View();
        }
        // GET: Player
        public async Task<IActionResult> SearchList(string sortOrder, string searchString, int? page)
        {
            var player = from players in _context.Player.Include(p => p.SportTeam) select players;

            ViewData["SortOrder"] = sortOrder;
            ViewData["SearchString"] = searchString;

            if(!String.IsNullOrEmpty(searchString)){
                player = player.Where(p => p.FullName.StartsWith(searchString) || p.SportTeam.Name.StartsWith(searchString));
            }

            switch(sortOrder){
                case "full_name_desc":
                    player = player.OrderByDescending(p => p.FullName);
                    break;
                case "full_name":
                    player = player.OrderBy(p => p.FullName);
                    break;
                case "first_name_desc":
                    player = player.OrderByDescending(p => p.FirstName);
                    break;
                case "first_name":
                    player = player.OrderBy(p => p.FirstName);
                    break;
                case "last_name_desc":
                    player = player.OrderByDescending(p => p.LastName);
                    break;
                case "last_name":
                    player = player.OrderBy(p => p.LastName);
                    break;
                case "sport_team_desc":
                    player = player.OrderByDescending(p => p.SportTeam.Name);
                    break;
                case "sport_team":
                    player = player.OrderBy(p => p.SportTeam.Name);
                    break;
                default:
                    player = player.OrderBy(p => p.FullName);
                    break;
            }

            int pageSize = 10;
            return PartialView("SearchList", await PaginatedList<Player>.CreateAsync(player.AsNoTracking(), page ?? 1, pageSize));

        }

        // GET: Player/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .Include(p => p.SportTeam)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Player/Create
        public IActionResult Create()
        {
            ViewData["SportTeamId"] = new SelectList(_context.SportTeam, "Id", "Name");
            return View();
        }

        // POST: Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SportTeamId,LastName,FirstName,FullName")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SportTeamId"] = new SelectList(_context.SportTeam, "Id", "Name", player.SportTeamId);
            return View(player);
        }

        // GET: Player/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player.SingleOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            ViewData["SportTeamId"] = new SelectList(_context.SportTeam, "Id", "Name", player.SportTeamId);
            return View(player);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SportTeamId,LastName,FirstName,FullName")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
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
            ViewData["SportTeamId"] = new SelectList(_context.SportTeam, "Id", "Name", player.SportTeamId);
            return View(player);
        }

        // GET: Player/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .Include(p => p.SportTeam)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Player.SingleOrDefaultAsync(m => m.Id == id);
            _context.Player.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Player.Any(e => e.Id == id);
        }
    }
}
