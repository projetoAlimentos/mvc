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
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Subject
        //[Authorize(Roles = "Administrador,Professor,Assistente,Aluno")]
        [HttpGet("/Subject")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Subject.ToListAsync());
        }

        // GET: Subject/Details/5
        [HttpGet("/Subject/{SubjectId}")]
        public async Task<IActionResult> Details(int? SubjectId)
        {
            if (SubjectId == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                    .SingleOrDefaultAsync(m => m.Id == SubjectId);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subject/Create
        [HttpGet("/Subject/Create")]
        [Authorize(Roles = "Administrador,Professor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Professor")]
        [HttpPost("/Subject/Create")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subject/Edit/5
        [Authorize(Roles = "Administrador,Professor")]
        [HttpGet("/Subject/Edit/{SubjectId}")]
        public async Task<IActionResult> Edit(int? SubjectId)
        {
            if (SubjectId == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject.SingleOrDefaultAsync(m => m.Id == SubjectId);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Professor")]
        [HttpPost("/Subject/Edit/{SubjectId}")]
        public async Task<IActionResult> Edit(int SubjectId, [Bind("Id,Name,Description")] Subject subject)
        {
            if (SubjectId != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.Id))
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
            return View(subject);
        }

        // GET: Subject/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpGet("/Subject/Delete/{SubjectId}")]
        public async Task<IActionResult> Delete(int? SubjectId)
        {
            if (SubjectId == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                    .SingleOrDefaultAsync(m => m.Id == SubjectId);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subject/Delete/5
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        [HttpPost("/Subject/Delete/{SubjectId}")]
        public async Task<IActionResult> DeleteConfirmed(int SubjectId)
        {
            var subject = await _context.Subject.SingleOrDefaultAsync(m => m.Id == SubjectId);
            _context.Subject.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int SubjectId)
        {
            return _context.Subject.Any(e => e.Id == SubjectId);
        }
    }
}
