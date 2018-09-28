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
    [Authorize(Roles = "Administrador")]
    public class AnswerAttemptController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnswerAttemptController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AnswerAttempt
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AnswerAttempt.Include(a => a.Question);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AnswerAttempt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerAttempt = await _context.AnswerAttempt
                .Include(a => a.Question)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (answerAttempt == null)
            {
                return NotFound();
            }

            return View(answerAttempt);
        }

        // GET: AnswerAttempt/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id");
            return View();
        }

        // POST: AnswerAttempt/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuestionId")] AnswerAttempt answerAttempt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answerAttempt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", answerAttempt.QuestionId);
            return View(answerAttempt);
        }

        // GET: AnswerAttempt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerAttempt = await _context.AnswerAttempt.SingleOrDefaultAsync(m => m.Id == id);
            if (answerAttempt == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", answerAttempt.QuestionId);
            return View(answerAttempt);
        }

        // POST: AnswerAttempt/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuestionId")] AnswerAttempt answerAttempt)
        {
            if (id != answerAttempt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answerAttempt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerAttemptExists(answerAttempt.Id))
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
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", answerAttempt.QuestionId);
            return View(answerAttempt);
        }

        // GET: AnswerAttempt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerAttempt = await _context.AnswerAttempt
                .Include(a => a.Question)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (answerAttempt == null)
            {
                return NotFound();
            }

            return View(answerAttempt);
        }

        // POST: AnswerAttempt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answerAttempt = await _context.AnswerAttempt.SingleOrDefaultAsync(m => m.Id == id);
            _context.AnswerAttempt.Remove(answerAttempt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerAttemptExists(int id)
        {
            return _context.AnswerAttempt.Any(e => e.Id == id);
        }
    }
}
