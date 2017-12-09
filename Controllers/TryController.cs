using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projeto.Data;
using projeto.Models;

namespace projeto.Controllers
{
    public class TryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Try
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Try.Include(t => t.topico);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Try/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ttry = await _context.Try
                .Include(t => t.topico)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ttry == null)
            {
                return NotFound();
            }

            return View(ttry);
        }

        // GET: Try/Create
        public IActionResult Create()
        {
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id");
            return View();
        }

        // POST: Try/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,TopicId")] Try ttry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ttry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id", ttry.TopicId);
            return View(ttry);
        }

        // GET: Try/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ttry = await _context.Try.SingleOrDefaultAsync(m => m.Id == id);
            if (ttry == null)
            {
                return NotFound();
            }
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id", ttry.TopicId);
            return View(ttry);
        }

        // POST: Try/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationUserId,TopicId")] Try ttry)
        {
            if (id != ttry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ttry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TryExists(ttry.Id))
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
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id", ttry.TopicId);
            return View(ttry);
        }

        // GET: Try/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ttry = await _context.Try
                .Include(t => t.topico)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ttry == null)
            {
                return NotFound();
            }

            return View(ttry);
        }

        // POST: Try/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ttry = await _context.Try.SingleOrDefaultAsync(m => m.Id == id);
            _context.Try.Remove(ttry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TryExists(int id)
        {
            return _context.Try.Any(e => e.Id == id);
        }
    }
}
