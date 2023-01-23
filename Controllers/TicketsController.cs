using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcTicket.Data;
using MvcTicket.Models;

namespace MvcTicket.Controllers
{
    public class TicketsController : Controller
    {
        private readonly MvcTicketContext _context;

        public TicketsController(MvcTicketContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index(string ticketType, string searchString)
        {
            if (_context.Ticket == null)
            {
                return Problem("Entity set 'MvcTicketContext.Ticket'  is null.");
            }

            IQueryable<string> typeQuery = from m in _context.Ticket
                                    orderby m.TicketType
                                    select m.TicketType;
            var tickets = from m in _context.Ticket
                        select m;

            //uwaga na to - może powodować błędy:
            if(!String.IsNullOrEmpty(searchString))
            {
                int searchId;
                if(int.TryParse(searchString, out searchId))
                {
                    tickets = tickets.Where(i => i.Id == searchId); //potencjalny problem z SQLite
                    //test: http://localhost:5017/Tickets?searchString=1 - pokazuje dobrze
                }
            }

            if (!string.IsNullOrEmpty(ticketType))
            {
                tickets = tickets.Where(x => x.TicketType == ticketType);
            }

            var ticketTypeVM = new TicketTypeViewModel
            {
                Types = new SelectList(await typeQuery.Distinct().ToListAsync()),
                Tickets = await tickets.ToListAsync()
            };

            return View(ticketTypeVM);
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ticket == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpirationDate,TicketType,IfDiscount,Price,IfZone")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ticket == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpirationDate,TicketType,IfDiscount,Price,IfZone")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ticket == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ticket == null)
            {
                return Problem("Entity set 'MvcTicketContext.Ticket'  is null.");
            }
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket != null)
            {
                _context.Ticket.Remove(ticket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
          return (_context.Ticket?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
