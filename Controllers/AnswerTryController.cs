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
    public class AnswerTryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnswerTryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AnswerTry
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AnswerTry.Include(a => a.Question);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AnswerTry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerTry = await _context.AnswerTry
                .Include(a => a.Question)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (answerTry == null)
            {
                return NotFound();
            }

            return View(answerTry);
        }

        // GET: AnswerTry/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id");
            return View();
        }

        // POST: AnswerTry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuestionId")] AnswerTry answerTry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answerTry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", answerTry.QuestionId);
            return View(answerTry);
        }

        // GET: AnswerTry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerTry = await _context.AnswerTry.SingleOrDefaultAsync(m => m.Id == id);
            if (answerTry == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", answerTry.QuestionId);
            return View(answerTry);
        }

        // POST: AnswerTry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuestionId")] AnswerTry answerTry)
        {
            if (id != answerTry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answerTry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerTryExists(answerTry.Id))
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
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", answerTry.QuestionId);
            return View(answerTry);
        }

        // GET: AnswerTry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerTry = await _context.AnswerTry
                .Include(a => a.Question)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (answerTry == null)
            {
                return NotFound();
            }

            return View(answerTry);
        }

        // POST: AnswerTry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answerTry = await _context.AnswerTry.SingleOrDefaultAsync(m => m.Id == id);
            _context.AnswerTry.Remove(answerTry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerTryExists(int id)
        {
            return _context.AnswerTry.Any(e => e.Id == id);
        }
    }
}
