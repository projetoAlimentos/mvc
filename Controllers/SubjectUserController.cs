using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projeto.Data;
using projeto.Models;

namespace projeto.Controllers
{
    [Authorize]
    public class SubjectUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubjectUser
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SubjectUser.Include(s => s.Subject);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SubjectUser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectUser = await _context.SubjectUser
                .Include(s => s.Subject)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (subjectUser == null)
            {
                return NotFound();
            }

            return View(subjectUser);
        }

        // GET: SubjectUser/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Id");
            return View();
        }

        // POST: SubjectUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,SubjectId")] SubjectUser subjectUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subjectUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Id", subjectUser.SubjectId);
            return View(subjectUser);
        }

        // GET: SubjectUser/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectUser = await _context.SubjectUser.SingleOrDefaultAsync(m => m.Id == id);
            if (subjectUser == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Id", subjectUser.SubjectId);
            return View(subjectUser);
        }

        // POST: SubjectUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,SubjectId")] SubjectUser subjectUser)
        {
            if (id != subjectUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subjectUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectUserExists(subjectUser.Id))
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
            ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Id", subjectUser.SubjectId);
            return View(subjectUser);
        }

        // GET: SubjectUser/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectUser = await _context.SubjectUser
                .Include(s => s.Subject)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (subjectUser == null)
            {
                return NotFound();
            }

            return View(subjectUser);
        }

        // POST: SubjectUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subjectUser = await _context.SubjectUser.SingleOrDefaultAsync(m => m.Id == id);
            _context.SubjectUser.Remove(subjectUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectUserExists(int id)
        {
            return _context.SubjectUser.Any(e => e.Id == id);
        }
    }
}
