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
    public class TicketPurchaseHistController : BaseController
    {
        public TicketPurchaseHistController(IDbContextService dbContextService) : base(dbContextService){}

        public IActionResult Index(string searchString){
            ViewData["SearchString"] = searchString;
            return View();
        }

        // GET: TicketPurchaseHist
        public async Task<IActionResult> SearchList(string sortOrder, string searchString, int? page)
        {

            var ticketPurchase = from ticketPurchases in _context.TicketPurchaseHist.Include(t => t.PurchasedBy).Include(t => t.SportingEventTicket).Include(t => t.TransferredFrom) select ticketPurchases;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString;

            if(!String.IsNullOrEmpty(searchString)){
                ticketPurchase = ticketPurchase.Where(s => s.PurchasedBy.FullName.StartsWith(searchString));
            }

            switch(sortOrder){
                case "price_desc":
                    ticketPurchase = ticketPurchase.OrderByDescending(t => t.PurchasePrice);
                    break;
                case "price":
                    ticketPurchase = ticketPurchase.OrderBy(t => t.PurchasePrice);
                    break;
                case "purchased_by_desc":
                    ticketPurchase = ticketPurchase.OrderByDescending(t => t.PurchasedBy.FullName);
                    break;
                case "purchased_by":
                    ticketPurchase = ticketPurchase.OrderBy(t => t.PurchasedBy.FullName);
                    break;
                case "seat_desc":
                    ticketPurchase = ticketPurchase.OrderByDescending(t => t.SportingEventTicket.Seat);
                    break;
                case "seat":
                    ticketPurchase = ticketPurchase.OrderBy(t => t.SportingEventTicket.Seat);
                    break;
                case "transferred_from_desc":
                    ticketPurchase = ticketPurchase.OrderByDescending(t => t.TransferredFrom.FullName);
                    break;
                case "transferred_from":
                    ticketPurchase = ticketPurchase.OrderBy(t => t.TransferredFrom.FullName);
                    break;
                default:
                    ticketPurchase = ticketPurchase.OrderBy(p => p.PurchasedBy.FullName);
                    break;
            }

            int pageSize = 10;
            return PartialView("SearchList", await PaginatedList<TicketPurchaseHist>.CreateAsync(ticketPurchase.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: TicketPurchaseHist/Details/5
        public async Task<IActionResult> Details(long? sportingEventTicketId, long? purchasedById, DateTime? transactionDateTime)
        {
            if (sportingEventTicketId == null || purchasedById == null || transactionDateTime == null)
            {
                return NotFound();
            }

            var ticketPurchaseHist = await _context.TicketPurchaseHist
                .Include(t => t.PurchasedBy)
                .Include(t => t.SportingEventTicket)
                .Include(t => t.TransferredFrom)
                .SingleOrDefaultAsync(m => m.SportingEventTicketId == sportingEventTicketId && m.PurchasedById == purchasedById && m.TransactionDateTime == transactionDateTime);
            if (ticketPurchaseHist == null)
            {
                return NotFound();
            }

            return View(ticketPurchaseHist);
        }

        // GET: TicketPurchaseHist/Create
        public IActionResult Create()
        {
            ViewData["PurchasedById"] = new SelectList(_context.Person, "Id", "FullName");
            ViewData["SportingEventTicketId"] = new SelectList(_context.SportingEventTicket, "Id", "Seat");
            ViewData["TransferredFromId"] = new SelectList(_context.Person, "Id", "FullName");
            return View();
        }

        // POST: TicketPurchaseHist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SportingEventTicketId,PurchasedById,TransactionDateTime,TransferredFromId,PurchasePrice")] TicketPurchaseHist ticketPurchaseHist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketPurchaseHist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PurchasedById"] = new SelectList(_context.Person, "Id", "FullName", ticketPurchaseHist.PurchasedById);
            ViewData["SportingEventTicketId"] = new SelectList(_context.SportingEventTicket, "Id", "Seat", ticketPurchaseHist.SportingEventTicketId);
            ViewData["TransferredFromId"] = new SelectList(_context.Person, "Id", "FullName", ticketPurchaseHist.TransferredFromId);
            return View(ticketPurchaseHist);
        }

        // GET: TicketPurchaseHist/Edit/5
        public async Task<IActionResult> Edit(long? sportingEventTicketId, long? purchasedById, DateTime? transactionDateTime)
        {
            if (sportingEventTicketId == null || purchasedById == null || transactionDateTime == null)
            {
                return NotFound();
            }

            var ticketPurchaseHist = await _context.TicketPurchaseHist
                .Include(t => t.PurchasedBy)
                .Include(t => t.SportingEventTicket)
                .Include(t => t.TransferredFrom)
                .SingleOrDefaultAsync(m => m.SportingEventTicketId == sportingEventTicketId && m.PurchasedById == purchasedById && m.TransactionDateTime == transactionDateTime);
            if (ticketPurchaseHist == null)
            {
                return NotFound();
            }
            ViewData["PurchasedById"] = new SelectList(_context.Person, "Id", "FullName", ticketPurchaseHist.PurchasedById);
            ViewData["SportingEventTicketId"] = new SelectList(_context.SportingEventTicket, "Id", "Seat", ticketPurchaseHist.SportingEventTicketId);
            ViewData["TransferredFromId"] = new SelectList(_context.Person, "Id", "FullName", ticketPurchaseHist.TransferredFromId);
            return View(ticketPurchaseHist);
        }

        // POST: TicketPurchaseHist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SportingEventTicketId,PurchasedById,TransactionDateTime,TransferredFromId,PurchasePrice")] TicketPurchaseHist ticketPurchaseHist)
        {
            if (id != ticketPurchaseHist.SportingEventTicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketPurchaseHist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketPurchaseHistExists(ticketPurchaseHist.SportingEventTicketId, ticketPurchaseHist.PurchasedById, ticketPurchaseHist.TransactionDateTime))
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
            ViewData["PurchasedById"] = new SelectList(_context.Person, "Id", "FullName", ticketPurchaseHist.PurchasedById);
            ViewData["SportingEventTicketId"] = new SelectList(_context.SportingEventTicket, "Id", "Seat", ticketPurchaseHist.SportingEventTicketId);
            ViewData["TransferredFromId"] = new SelectList(_context.Person, "Id", "FullName", ticketPurchaseHist.TransferredFromId);
            return View(ticketPurchaseHist);
        }

        // GET: TicketPurchaseHist/Delete/5
        public async Task<IActionResult> Delete(long? sportingEventTicketId, long? purchasedById, DateTime? transactionDateTime)
        {
            if (sportingEventTicketId == null || purchasedById == null || transactionDateTime == null)
            {
                return NotFound();
            }

            var ticketPurchaseHist = await _context.TicketPurchaseHist
                .Include(t => t.PurchasedBy)
                .Include(t => t.SportingEventTicket)
                .Include(t => t.TransferredFrom)
                .SingleOrDefaultAsync(m => m.SportingEventTicketId == sportingEventTicketId && m.PurchasedById == purchasedById && m.TransactionDateTime == transactionDateTime);
            if (ticketPurchaseHist == null)
            {
                return NotFound();
            }

            return View(ticketPurchaseHist);
        }

        // POST: TicketPurchaseHist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? sportingEventTicketId, long? purchasedById, DateTime? transactionDateTime)
        {
            var ticketPurchaseHist = await _context.TicketPurchaseHist.SingleOrDefaultAsync(m => m.SportingEventTicketId == sportingEventTicketId && m.PurchasedById == purchasedById && m.TransactionDateTime == transactionDateTime);
            _context.TicketPurchaseHist.Remove(ticketPurchaseHist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketPurchaseHistExists(long? sportingEventTicketId, long? purchasedById, DateTime? transactionDateTime)
        {
            return _context.TicketPurchaseHist.Any(m => m.SportingEventTicketId == sportingEventTicketId && m.PurchasedById == purchasedById && m.TransactionDateTime == transactionDateTime);
        }
    }
}
